using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingProjectFrontEnd.Controllers;

public class PhysicianNavigationController : Controller
{
    public IActionResult PhysicianDashboard()
    {
        return View();
    }
}
