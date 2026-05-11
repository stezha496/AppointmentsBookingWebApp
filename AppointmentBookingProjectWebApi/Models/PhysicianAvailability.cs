namespace AppointmentBookingProjectWebApi.Models;

public class PhysicianAvailability
{
    public int Id { get; set; }

    public int? PhysicianId { get; set; }
    public Physician? Physician { get; set; }

    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    public bool? IsAvailable { get; set; }

}
