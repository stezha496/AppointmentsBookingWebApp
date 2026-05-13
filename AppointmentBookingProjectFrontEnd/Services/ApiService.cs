using AppointmentBookingProjectFrontEnd.Models.Dtos;
using System.Net;
using System.Text;
using System.Text.Json;

namespace AppointmentBookingProjectFrontEnd.Services;

/// <summary>
/// Helper class that sends http requests to the web api
/// </summary>
public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly CookieContainer _cookieContainer;

    public ApiService(
        IHttpClientFactory httpClientFactory,
        CookieContainer cookieContainer
        )
    {
        _httpClient = httpClientFactory.CreateClient("AppointmentBookingAPI");
        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _cookieContainer = cookieContainer;
    }

    // Helpers
    #region HelperFunctions
    private StringContent Serialize(object data)
    {
        string json = JsonSerializer.Serialize(data);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    public async Task<T?> Deserialize<T>(HttpResponseMessage response)
    {
        string raw = await response.Content.ReadAsStringAsync();

        // If expecting a string and it's not wrapped in quotes, wrap it
        if (typeof(T) == typeof(string) && !raw.StartsWith("\""))
            raw = $"\"{raw}\"";

        return JsonSerializer.Deserialize<T>(raw, _jsonOptions);
    }
    #endregion

    #region Login/Logout
    public async Task<HttpResponseMessage> Login(LoginDto model)
    {
        // Check cookies received
        //var cookies = _cookieContainer.GetCookies(new Uri("http://localhost:5081/"));
        //Console.WriteLine($"Cookies after login: {cookies.Count}");
        //foreach (Cookie cookie in cookies)
        //    Console.WriteLine($"Cookie: {cookie.Name} = {cookie.Value}");
        return await _httpClient.PostAsync("api/appuser/login", Serialize(model));
    }

    public async Task<HttpResponseMessage> Logout()
    {
        return await _httpClient.PostAsync("api/appuser/logout", null);
    }
    #endregion

    #region User Identity
    public async Task<HttpResponseMessage> GetCurrentUser()
    {
        return await _httpClient.GetAsync("api/appusers/current-user");
    }

    // Gets Identity Role for the user
    public async Task<HttpResponseMessage> GetUserRole(string userId)
    {
        return await _httpClient.GetAsync($"api/appusers/{userId}/role");
    }
    #endregion

    #region Bookings
    public async Task<HttpResponseMessage> GetBookingsByPatientUsername(string patientUsername)
    {
        return await _httpClient.GetAsync($"patient/bookings/username/{patientUsername}");
    }

    public async Task<HttpResponseMessage> CreateBooking(CreateBookingDto model)
    {
        string json = JsonSerializer.Serialize(model);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        Console.WriteLine($"Body: {json}");

        return await _httpClient.PostAsync("patient/booking/create", content);
    }

    public async Task<HttpResponseMessage> GetBookingsByPhysicianId(int physicianId)
    {
        return await _httpClient.GetAsync($"physician/upcoming-bookings/{physicianId}");
    }
    #endregion

    #region Patients
    public async Task<HttpResponseMessage> GetAllPhysicians()
    {
        return await _httpClient.GetAsync($"patient/all-physicians");
    }

    public async Task<HttpResponseMessage> GetPatientIdByUsername(string username)
    {
        return await _httpClient.GetAsync($"patient/id/{username}");
    }


    #endregion

    #region Physician Availability
    public async Task<HttpResponseMessage> GetPhysicianAvailability(int physicianId)
    {
        return await _httpClient.GetAsync($"patient/physician-all-availabilities/{physicianId}");
    }
    #endregion

    #region Physician

    public async Task<HttpResponseMessage> GetPhysicianIdByUsername(string username)
    {
        return await _httpClient.GetAsync($"physician/id/{username}");
    }

    #endregion
}
