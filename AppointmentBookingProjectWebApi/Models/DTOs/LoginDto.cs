using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingProjectWebApi.Models.DTOs;
/// <summary>
/// For login
/// </summary>
public class LoginDto
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Password { get; set; }
}
