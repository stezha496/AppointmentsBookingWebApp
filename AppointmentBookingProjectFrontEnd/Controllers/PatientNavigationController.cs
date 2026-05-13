using AppointmentBookingProjectFrontEnd.Models.Dtos;
using AppointmentBookingProjectFrontEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingProjectFrontEnd.Controllers;

/// <summary>
/// Handles actions done by the Patient user
/// </summary>
public class PatientNavigationController : Controller
{
    private readonly ApiService _apiService;

    public PatientNavigationController(ApiService apiService)
    {
        _apiService = apiService;
    }

    [HttpGet]
    public async Task<IActionResult> ChoosePhysician()
    {
        HttpResponseMessage response = await _apiService.GetAllPhysicians();
        List<PhysicianDto>? physicians = 
            await _apiService.Deserialize<List<PhysicianDto>>(response);
        
        return View(physicians);
    }        

    [HttpGet]
    public IActionResult PatientDashboard()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> PatientBookings()
    {
        string? userName = HttpContext.Session.GetString("UserName");
        HttpResponseMessage response = await _apiService.GetBookingsByPatientUsername(userName);

        List<BookingDto>? bookings = await _apiService.Deserialize<List<BookingDto>>(response);

        return View(bookings);
    }

    [HttpGet]
    public async Task<IActionResult> BookAppointment(int physicianId, string physicianName)
    {
        // Save selected physician information
        HttpContext.Session.SetString("SelectedPhysicianId", physicianId.ToString());
        HttpContext.Session.SetString("SelectedPhysicianName", physicianName);

        HttpResponseMessage response = await _apiService.GetPhysicianAvailability(physicianId);
        List<PhysicianAvailabilityDto>? availabilities =
            await _apiService.Deserialize<List<PhysicianAvailabilityDto>>(response);

        return View(availabilities ?? new List<PhysicianAvailabilityDto>());
    }

    [HttpPost]
    public async Task<IActionResult> BookAppointment(CreateBookingDto dto)
    {
        string? patientId = HttpContext.Session.GetString("UserId");
        dto.PatientId = int.TryParse(patientId, out int id) ? id : null;

        // Validate a time was selected
        if (dto.BookedTimeStart == default)
        {
            ModelState.AddModelError("", "Please select a time before booking.");
            return View();
        }

        // Validate reason for visit
        if (string.IsNullOrWhiteSpace(dto.ReasonForVisit))
        {
            ModelState.AddModelError("", "Please enter a reason for visiting.");
            return View();
        }

        HttpResponseMessage response = await _apiService.CreateBooking(dto);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "Failed to book appointment. Please try again.");
            return View();
        }

        return RedirectToAction("BookingSuccess");
    }

    [HttpGet]
    public IActionResult BookingSuccess()
    {
        return View();
    }
}
