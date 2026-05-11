using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingProjectWebApi.Models;

public class Patient
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public double? Height { get; set; }
    public double? Weight { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
}
