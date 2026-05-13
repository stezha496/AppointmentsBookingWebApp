using AppointmentBookingProjectWebApi.Mappings;
using AppointmentBookingProjectWebApi.Models;
using AppointmentBookingProjectWebApi.Models.DtoMapping;
using AppointmentBookingProjectWebApi.Models.DTOs;
using AppointmentBookingProjectWebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingProjectWebApi.Controllers;

/**
 * Features managed:
 * - View current bookings
 * - View all physicians
 * - View all availabilities for a given physician
 * - Post form for patient details and reason for visiting
 */
//[Authorize(Roles = "Patient")]
[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    // Inject repository
    private readonly IPhysicianRepository _physicianRepository;
    private readonly IPhysicianAvailabilityRepository _physicianAvailabilityRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IPatientDetailsRepository _patientDetailsRepository;

    public PatientController(
    IBookingRepository bookingRepository,
    IPatientRepository patientRepository,
    IPhysicianRepository physicianRepository,
    IPhysicianAvailabilityRepository physicianAvailabilityRepository,
    IPatientDetailsRepository patientDetailsRepository
    //UserManager<IdentityUser> userManager
    )
    {
        _bookingRepository = bookingRepository;
        _patientRepository = patientRepository;
        _physicianRepository = physicianRepository;
        _physicianAvailabilityRepository = physicianAvailabilityRepository;
        _patientDetailsRepository = patientDetailsRepository;
        //this.userManager = userManager;
    }

    [HttpGet("id/{username}")]
    public async Task<IActionResult> GetPatientIdByUsername(string username)
    {
        int? id = await _patientRepository.GetPatientIdByUsername(username);

        return Ok(id);
    }

    [HttpGet("all-physicians")]
    public async Task<IActionResult> GetAllPhysicians()
    {
        List<Physician> allPhysicians = await _physicianRepository.GetAllPhysicians();
        List<PhysicianDto> allPhysicianDtos = PhysicianMapping.ToDtoList(allPhysicians);

        return Ok(allPhysicianDtos);
    }

    [HttpGet("get-every-physician-availability")]
    public async Task<IActionResult> GetAllPhysicianAvailability()
    {
        List<PhysicianAvailability> allAvailability = await
            _physicianAvailabilityRepository.GetAllPhysicianAvailability();
        return Ok(allAvailability);
    }

    [HttpGet("physician-all-availabilities/{physicianId}")]
    public async Task<IActionResult> GetAllAvailabilitiesByPhysician(int physicianId)
    {
        List<PhysicianAvailability> availabilities = 
            await _physicianAvailabilityRepository.GetAllAvailabilitiesByPhysician(physicianId);
        List<PhysicianAvailabilityDto> availabilitiesDto = 
            PhysiciansAvailabilitiesMapping.ToDtoList(availabilities);

        return Ok(availabilitiesDto);
    }

    [HttpGet("bookings/patientId/{patientId}")]
    public async Task<IActionResult> GetBookingsByPatient(int patientId)
    {
        List<Booking> bookings = await _bookingRepository.GetBookingsByPatient(patientId);
        List<BookingDto> bookingDtos = BookingMapping.ToBookingDtoList(bookings);

        return Ok(bookingDtos);
    }

    [HttpGet("bookings/username/{patientUsername}")]
    public async Task<IActionResult> GetBookingsByPatientUsername(string patientUsername)
    {
        List<Booking> bookings = await _bookingRepository.GetBookingsByPatientUsername(patientUsername);
        List<BookingDto> bookingDtos = BookingMapping.ToBookingDtoList(bookings);

        return Ok(bookingDtos);
    }

    [HttpGet("bookings/availabilities/{physicianId}")]
    public async Task<IActionResult> GetPhysicianAvailabilities(int physicianId)
    {
        List<PhysicianAvailability> availabilities = 
            await _physicianAvailabilityRepository.GetAllTimeSlotsByPhysician(physicianId);
        return Ok(availabilities);
    }

    [HttpPost("booking/create")]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto bookingDto)
    {
        // Issue might occur here with Booking Id value
        Booking booking = BookingMapping.ToCreateBooking(bookingDto);
        // Add Patient field
        if (bookingDto.PatientId != null)
        {
            booking.Patient = await _patientRepository.GetPatientById((int)bookingDto.PatientId);
        }

        // Update physician availability
        if (bookingDto.AvailabilityId != null)
        {
            await _physicianAvailabilityRepository.SetAvailabilityBooked(
                bookingDto.AvailabilityId.Value
                );
        }

        // Save patient details by mapping dto -> patientdetails
        // TODO: create a mapping class for patient details and replace this
        PatientDetails patientDetails = new PatientDetails
        {
            Age = bookingDto.Age,
            Gender = bookingDto.Gender,
            Height = bookingDto.Height,
            Weight = bookingDto.Weight,
            PhoneNumber = bookingDto.PhoneNumber,
            patientId = bookingDto.PatientId,
            Patient = booking.Patient
        };
        await _patientDetailsRepository.AddPatientDetails(patientDetails);

        // Update physician availability
        if (bookingDto.AvailabilityId != null)
            await _physicianAvailabilityRepository.SetAvailabilityBooked(bookingDto.AvailabilityId.Value);

        await _bookingRepository.CreateBooking(booking);
        return Ok();
    }

    [HttpPost("details")]
    public async Task<IActionResult> AddDetails([FromBody] PatientDetailsDto patientDetailsDto)
    {
        PatientDetails patientDetails = PatientDetailsMapping.ToPatientDetails(patientDetailsDto);
        // Add Patient field
        patientDetails.Patient = await _patientRepository.GetPatientById(patientDetailsDto.PatientId);

        await _patientDetailsRepository.AddPatientDetails(patientDetails);
        return Ok(patientDetails);
    }
}
