using AppointmentBookingProjectFrontEnd.Models;
using AppointmentBookingProjectFrontEnd.Models.Dtos;
using AppointmentBookingProjectFrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppointmentBookingProjectFrontEnd.Controllers;

/// <summary>
/// Handles login, logout, and error page navigation
/// </summary>
/// <param name="apiService"></param>
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

    /// <summary>
    /// Authenticates the user and redirects them to the appropriate dashboard
    /// based on their assigned role.
    /// </summary>
    /// <param name="model">User login credentials.</param>
    /// <returns>
    /// </returns>
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
        HttpContext.Session.SetString("Email", loginResult.Email ?? "");
        HttpContext.Session.SetString("UserName", loginResult.Username ?? "");
        HttpContext.Session.SetString("UserRole", loginResult.Role ?? "");

        // Redirect and save id based on role
        if (loginResult.Role == "patient")
        {
            // For now, need to retrieve ID from Patient table seperately
            HttpResponseMessage patientIdResponse =
                await _apiService.GetPatientIdByUsername(loginResult.Username);
            string? patientId = await _apiService.Deserialize<string?>(patientIdResponse);
            HttpContext.Session.SetString("UserId", patientId);


            return RedirectToAction("PatientDashboard", "PatientNavigation");
        }
        else if (loginResult.Role == "physician")
        {
            // For now, need to retrieve ID from Physician table seperately
            HttpResponseMessage physicianIdResponse =
                await _apiService.GetPhysicianIdByUsername(loginResult.Username);
            string? physicianId = await _apiService.Deserialize<string?>(physicianIdResponse);
            HttpContext.Session.SetString("UserId", physicianId);

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
