using AppointmentBookingProjectWebApi.Models;
using AppointmentBookingProjectWebApi.Models.DtoMapping;
using AppointmentBookingProjectWebApi.Models.DTOs;
using AppointmentBookingProjectWebApi.Repositories;
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

    [HttpGet("all-physicians")]
    public async Task<IActionResult> GetAllPhysicians()
    {
        List<Physician> allPhysicians = await _physicianRepository.GetAllPhysicians();
        return Ok(allPhysicians);
    }

    [HttpGet("all-physician-availabilities")]
    public async Task<IActionResult> GetAllPhysicianAvailability()
    {
        List<PhysicianAvailability> allAvailability = await
            _physicianAvailabilityRepository.GetAllPhysicianAvailability();
        return Ok(allAvailability);
    }

    [HttpGet("bookings/patient/{patientId}")]
    public async Task<IActionResult> GetBookingsByPatient(int patientId)
    {
        List<Booking> bookings = await _bookingRepository.GetBookingsByPatient(patientId);
        return Ok(bookings);
    }

    [HttpGet("bookings/availabilities/{physicianId}")]
    public async Task<IActionResult> GetPhysicianAvailabilities(int physicianId)
    {
        List<PhysicianAvailability> availabilities = 
            await _physicianAvailabilityRepository.GetAvailabilitiesByPhysician(physicianId);
        return Ok(availabilities);
    }

    [HttpPost("bookings/create")]
    public async Task<IActionResult> CreateBooking([FromBody] BookingDto bookingDto)
    {
        Booking booking = BookingMapping.ToBooking(bookingDto);
        // Add Patient field
        booking.Patient = await _patientRepository.GetPatientById(bookingDto.PatientId);

        await _bookingRepository.CreateBooking(booking);
        return Ok(booking);
    }

    [HttpPost("details")]
    public async Task<IActionResult> AddDetails([FromBody] PatientDetailsDto patientDetailsDto)
    {
        PatientDetails patientDetails = PatientDetailsMapping.ToPatientDetails(patientDetailsDto);
        // Add Patient field
        patientDetails.Patient = await _patientRepository.GetPatientById(patientDetailsDto.patientId);

        await _patientDetailsRepository.AddPatientDetails(patientDetails);
        return Ok(patientDetails);
    }
}
