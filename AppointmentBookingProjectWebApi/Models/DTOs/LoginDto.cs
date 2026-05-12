using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingProjectWebApi.Models.DTOs;

public class LoginDto
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Password { get; set; }
}
