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
 */

/// <summary>
/// Manages Physician related features that interact with the database
/// </summary>
//[Authorize(Roles = "Physician")]
[ApiController]
[Route("[controller]")]
public class PhysicianController : ControllerBase
{
    // Inject repository
    private readonly IBookingRepository _bookingRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IPhysicianRepository _physicianRepository;
    private readonly IPhysicianAvailabilityRepository _physicianAvailabilityRepository;
    private readonly IPatientDetailsRepository _patientDetailsRepository;
    private UserManager<IdentityUser> userManager;

    public PhysicianController(
        IBookingRepository bookingRepository,
        IPatientRepository patientRepository,
        IPatientDetailsRepository patientDetailsRepository,
        IPhysicianRepository physicianRepository,
        IPhysicianAvailabilityRepository physicianAvailabilityRepository,
        UserManager<IdentityUser> userManager
        )
    {
        _bookingRepository = bookingRepository;
        _patientRepository = patientRepository;
        _physicianRepository = physicianRepository;
        _physicianAvailabilityRepository = physicianAvailabilityRepository;
        _patientDetailsRepository = patientDetailsRepository;
        this.userManager = userManager;
    }

    [HttpGet("all-bookings")]
    public async Task<IActionResult> GetAllBookings()
    {
        List<Booking> allBookings = await _bookingRepository.GetAllBookings();
        return Ok(allBookings);
    }

    [HttpGet("all-patients")]
    public async Task<IActionResult> GetAllPatients()
    {
        List<Patient> allPatients = await _patientRepository.GetAllPatients();
        return Ok(allPatients);
    }

    [HttpGet("all-physicians")]
    public async Task<IActionResult> GetAllPhysicians()
    {
        List<Physician> allPhysicians = await _physicianRepository.GetAllPhysicians();
        return Ok(allPhysicians);
    }

    [HttpGet("upcoming-bookings/{physicianId}")]
    public async Task<IActionResult> GetBookingsByPhysician(int physicianId)
    {
        List<Booking> bookings = 
            await _bookingRepository.GetUpcomingBookingsByPhysician(physicianId);

        //
        List<BookingDto> bookingsDto = BookingMapping.ToBookingDtoList(bookings);

        return Ok(bookingsDto);
    }

    [HttpGet("details/patient/{patientId}")]
    public async Task<IActionResult> GetPatientDetails(int patientId)
    {
        List<PatientDetails> patientDetails =
            await _patientDetailsRepository.GetAllPatientDetailsForPatient(patientId);

        return Ok(patientDetails);
    }

    [HttpGet("id/{username}")]
    public async Task<IActionResult> GetPhysicianIdByUsername(string username)
    {
        int? id = await _physicianRepository.GetPhysicianIdByUsername(username);

        return Ok(id);
    }
}
