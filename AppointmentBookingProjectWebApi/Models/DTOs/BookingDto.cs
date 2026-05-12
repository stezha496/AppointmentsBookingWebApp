using AppointmentBookingProjectWebApi.Enums;

namespace AppointmentBookingProjectWebApi.Models.DTOs;

public class BookingDto
{
    public string ReasonForVisit { get; set; }
    public DateTime BookedTimeStart { get; set; }
    // This is in minutes
    public int BookedTimeDuration { get; set; }
    public int PhysicianId { get; set; }
    public int PatientId { get; set; }
    public DateTime Created { get; set; }
    public string Status { get; set; }   
}
