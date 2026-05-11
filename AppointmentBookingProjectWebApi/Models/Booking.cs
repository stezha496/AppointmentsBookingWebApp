using AppointmentBookingProjectWebApi.Enums;

namespace AppointmentBookingProjectWebApi.Models;

public class Booking
{
    public int Id { get; set; }

    // Status will be Pending, Canceled or Confirmed
    public BookingStatus Status { get; set; } = BookingStatus.Pending;

    public string? ReasonForVisit { get; set; }

    // This will be set at the time of creation to DateTime.Now
    public DateTime? Created { get; set; }

    public PatientDetails? details { get; set; }

    public int? PhysicianId { get; set; }
    public Physician? Physician { get; set; }

    public int? PatientId { get; set; }
    public Patient? Patient { get; set; }
}