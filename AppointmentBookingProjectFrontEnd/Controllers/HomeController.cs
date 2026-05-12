using AppointmentBookingProjectFrontEnd.Models;
using AppointmentBookingProjectFrontEnd.Models.Dtos;
using AppointmentBookingProjectFrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppointmentBookingProjectFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiService _apiService;

        public HomeController(ApiService apiService)
        {
            _apiService = apiService;
        }

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Step 1: Login
            HttpResponseMessage loginResponse = await _apiService.Login(model);

            if (!loginResponse.IsSuccessStatusCode)
            {
                string error = await loginResponse.Content.ReadAsStringAsync();
                ModelState.AddModelError("", error);
                return View(model);
            }

            // Step 2: Extract everything from the login response directly
            var loginResult = await _apiService.Deserialize<LoginResponseDto>(loginResponse);

            if (loginResult == null)
                return RedirectToAction("Error", "Home");

            // Step 3: Store in session
            HttpContext.Session.SetString("Emaik", loginResult.Email ?? "");
            HttpContext.Session.SetString("UserName", loginResult.Username ?? "");
            HttpContext.Session.SetString("UserRole", loginResult.Role ?? "");

            if (loginResult.Role == "patient")
            {
                return RedirectToAction("PatientDashboard", "PatientNavigation");
            }
            else if (loginResult.Role == "physician")
            {
                return RedirectToAction("PhysicianDashboard", "PhysicianNavigation");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpResponseMessage response = await _apiService.Logout();

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Login", "Home");
        }

        #endregion
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
