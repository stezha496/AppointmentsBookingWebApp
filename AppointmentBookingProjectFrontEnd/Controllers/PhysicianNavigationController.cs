using AppointmentBookingProjectFrontEnd.Models.Dtos;
using AppointmentBookingProjectFrontEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingProjectFrontEnd.Controllers;

public class PhysicianNavigationController : Controller
{
    private readonly ApiService _apiService;

    public PhysicianNavigationController(ApiService apiService)
    {
        _apiService = apiService;
    }

    [HttpGet]
    public IActionResult PhysicianDashboard()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> PhysicianUpcomingAppointments()
    {
        string? physicianId = HttpContext.Session.GetString("UserId");
        int physicianIdValue = int.TryParse(physicianId, out int id) ? id : 0;

        HttpResponseMessage response = await _apiService.GetBookingsByPhysicianId(physicianIdValue);
        List<BookingDto>? bookings = await _apiService.Deserialize<List<BookingDto>>(response);
        return View(bookings ?? new List<BookingDto>());
    }

}
