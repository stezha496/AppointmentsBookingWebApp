namespace AppointmentBookingProjectWebApi.Models;
// TODO use IdentityUser instead
public class Physician
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
}
