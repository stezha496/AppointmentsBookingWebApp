namespace AppointmentBookingProjectWebApi.Models.DTOs;

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
