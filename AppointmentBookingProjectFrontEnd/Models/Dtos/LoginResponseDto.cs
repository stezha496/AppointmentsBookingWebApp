using System.Text.Json.Serialization;

namespace AppointmentBookingProjectFrontEnd.Models.Dtos;

public class LoginResponseDto
{
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("role")]
    public string? Role { get; set; }
}
