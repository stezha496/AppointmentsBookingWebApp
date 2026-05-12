using AppointmentBookingProjectWebApi.Models;
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

    public PatientController(
    IBookingRepository bookingRepository,
    //IPatientRepository patientRepository,
    IPhysicianRepository physicianRepository,
    IPhysicianAvailabilityRepository physicianAvailabilityRepository
    //UserManager<IdentityUser> userManager
    )
    {
        _bookingRepository = bookingRepository;
        //_patientRepository = patientRepository;
        _physicianRepository = physicianRepository;
        _physicianAvailabilityRepository = physicianAvailabilityRepository;
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
}
