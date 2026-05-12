using AppointmentBookingProjectFrontEnd.Models.Dtos;
using System.Net;
using System.Text;
using System.Text.Json;

namespace AppointmentBookingProjectFrontEnd.Services;

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
}
