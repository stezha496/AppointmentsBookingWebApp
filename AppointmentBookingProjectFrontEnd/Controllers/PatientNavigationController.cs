using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingProjectFrontEnd.Controllers;

public class PatientNavigationController : Controller
{
    public IActionResult ChoosePhysician()
    {
        return View();
    }

    [HttpGet]
    public IActionResult PatientDashboard()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ViewMyBookings()
    {
        return View();
    }

    [HttpGet]
    public IActionResult BookAppointment()
    {
        return View();
    }
}
