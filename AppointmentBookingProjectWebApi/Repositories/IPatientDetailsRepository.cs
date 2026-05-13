using AppointmentBookingProjectWebApi.Models;

namespace AppointmentBookingProjectWebApi.Repositories;

public interface IPatientDetailsRepository
{
    Task<List<PatientDetails>> GetAllPatientDetails();
    Task<List<PatientDetails>> GetAllPatientDetailsForPatient(int patientId);
    Task<List<PatientDetails>> GetAllPatientDetailsForPatientByUsername(string patientUsername);
    Task AddPatientDetails(PatientDetails patientDetails);
}
