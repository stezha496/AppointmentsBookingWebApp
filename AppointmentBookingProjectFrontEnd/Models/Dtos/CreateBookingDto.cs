namespace AppointmentBookingProjectFrontEnd.Models.Dtos;

/// <summary>
/// Sent by front end to webapi for creating a Booking
/// </summary>
public class CreateBookingDto
{
    public string? ReasonForVisit { get; set; }
    public DateTime? BookedTimeStart { get; set; }
    public int? BookedTimeDuration { get; set; }
    public int? PhysicianId { get; set; }
    public int? PatientId { get; set; }

    // For Physician Availability
    public int? AvailabilityId { get; set; }

    // Patient Details
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public double? Height { get; set; }
    public double? Weight { get; set; }
    public string? PhoneNumber { get; set; }
}
