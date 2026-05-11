namespace AppointmentBookingProjectWebApi.Models;

public class Physician
{
    public int Id { get; set; }

    public List<PhysicianAvailability>? PhysicianAvailabilities { get; set; }

    public string? Name { get; set; }
    public List<Booking>? CurrentBookings { get; set; }

    public string? PhoneNumber { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
}
