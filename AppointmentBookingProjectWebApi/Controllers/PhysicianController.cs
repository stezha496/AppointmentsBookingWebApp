using AppointmentBookingProjectWebApi.Models;
using AppointmentBookingProjectWebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingProjectWebApi.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
public class PhysicianController : ControllerBase
{
    // Inject repository
    private readonly IBookingRepository _bookingRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IPhysicianRepository _physicianRepository;
    private readonly IPhysicianAvailabilityRepository _physicianAvailabilityRepository;
    private UserManager<IdentityUser> userManager;

    public PhysicianController(
        IBookingRepository bookingRepository,
        IPatientRepository patientRepository,
        IPhysicianRepository physicianRepository,
        IPhysicianAvailabilityRepository physicianAvailabilityRepository,
        UserManager<IdentityUser> userManager
        )
    {
        _bookingRepository = bookingRepository;
        _patientRepository = patientRepository;
        _physicianRepository = physicianRepository;
        _physicianAvailabilityRepository = physicianAvailabilityRepository;
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

    [HttpGet("all-physician-availabilities")]
    public async Task<IActionResult> GetAllPhysicianAvailability()
    {
        List<PhysicianAvailability> allAvailability = await 
            _physicianAvailabilityRepository.GetAllPhysicianAvailability();
        return Ok(allAvailability);
    }
}
