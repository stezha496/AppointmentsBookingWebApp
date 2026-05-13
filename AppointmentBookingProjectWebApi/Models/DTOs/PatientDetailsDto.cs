namespace AppointmentBookingProjectWebApi.Models.DTOs;

/// <summary>
/// Used to represent Patient Details
/// </summary>
public class PatientDetailsDto
{
    public int? PatientDetailsId { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public string PhoneNumber { get; set; }
    public int PatientId { get; set; }
}
