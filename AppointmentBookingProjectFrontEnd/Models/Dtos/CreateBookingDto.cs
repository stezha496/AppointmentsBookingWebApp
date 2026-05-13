namespace AppointmentBookingProjectFrontEnd.Models.Dtos;

// Sent by front end
public class CreateBookingDto
{
    public string? ReasonForVisit { get; set; }
    public DateTime? BookedTimeStart { get; set; }
    public int? BookedTimeDuration { get; set; }
    public int? PhysicianId { get; set; }
    public int? PatientId { get; set; }
    public int? AvailabilityId { get; set; }
}
