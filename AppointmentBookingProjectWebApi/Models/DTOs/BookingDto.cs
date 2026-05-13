using AppointmentBookingProjectWebApi.Enums;

namespace AppointmentBookingProjectWebApi.Models.DTOs;

/// <summary>
/// Sent by webapi to front end
/// WebAPI -> Front End
/// </summary>
public class BookingDto
{
    public int? BookingId { get; set; }
    public string? ReasonForVisit { get; set; }
    public DateTime? BookedTimeStart { get; set; }
    public int? BookedTimeDuration { get; set; }
    public int? PhysicianId { get; set; }
    public int? PatientId { get; set; }
    public string? PatientName { get; set; }
    public DateTime Created { get; set; }    // always set by server
    public string? Status { get; set; }      // always set by server
}
