namespace AppointmentBookingProjectWebApi.Models.DTOs;

/// <summary>
/// Used to represent Physician Availability
/// </summary>
public class PhysicianAvailabilityDto
{
    public int? Id { get; set; }

    public int? PhysicianId { get; set; }
    public string PhysicianName { get; set; }

    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    public bool? IsAvailable { get; set; }
}
