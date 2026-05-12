using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingProjectFrontEnd.Controllers;

public class PatientNavigationController : Controller
{
    public IActionResult PatientDashboard()
    {
        return View();
    }
}
