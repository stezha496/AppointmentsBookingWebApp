using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingProjectFrontEnd.Models.Dtos;

/// <summary>
/// Password and username for login
/// </summary>
public class LoginDto
{
    [Required(ErrorMessage = "Please enter your username")]
    public required string UserName { get; set; }

    [Required(ErrorMessage = "Please enter your password")]
    public required string Password { get; set; }
}
