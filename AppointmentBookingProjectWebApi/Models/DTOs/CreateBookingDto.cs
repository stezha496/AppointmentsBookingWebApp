namespace AppointmentBookingProjectWebApi.Models.DTOs;

/// <summary>
/// Recieved by webapi from front end
/// Front end -> WebAPI
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
