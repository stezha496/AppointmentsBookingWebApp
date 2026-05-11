namespace AppointmentBookingProjectWebApi.Models;

public class PatientDetails
{
    public int Id { get; set; }

    public string? Notes { get; set; }

    public Patient? Patient { get; set; }
    public int? PatientId { get; set; }
}
