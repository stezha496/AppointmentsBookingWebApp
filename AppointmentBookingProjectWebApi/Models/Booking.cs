using AppointmentBookingProjectWebApi.Enums;

namespace AppointmentBookingProjectWebApi.Models;

/// <summary>
/// Entity representing a appointment booking
/// </summary>
public class Booking
{
    public int Id { get; set; }

    // Status will be Pending, Canceled or Confirmed
    public BookingStatus Status { get; set; } = BookingStatus.Pending;

    public string? ReasonForVisit { get; set; }

    // This will be set at the time of creation
    public DateTime Created { get; set; } = DateTime.Now;

    public DateTime? BookedTimeStart { get; set; }

    // This is in minutes
    public int? BookedTimeDuration { get; set; }

    public int? PhysicianId { get; set; }
    public Physician? Physician { get; set; }

    public int? PatientId { get; set; }
    public Patient? Patient { get; set; }
}