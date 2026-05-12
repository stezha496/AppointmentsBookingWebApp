namespace AppointmentBookingProjectWebApi.Models;

// A new one is created each time a patient books a appointment
public class PatientDetails
{
    public int Id { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public double? Height { get; set; }
    public double? Weight { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;

    public int? patientId { get; set; }
    public Patient? Patient { get; set; }
}
