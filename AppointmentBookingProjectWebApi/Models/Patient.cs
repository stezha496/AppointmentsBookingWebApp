using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingProjectWebApi.Models;

public class Patient
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
}
