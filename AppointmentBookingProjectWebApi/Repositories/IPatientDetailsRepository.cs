using AppointmentBookingProjectWebApi.Models;

namespace AppointmentBookingProjectWebApi.Repositories;

public interface IPatientDetailsRepository
{
    Task<List<PatientDetails>> GetAllPatientDetails();
    Task<List<PatientDetails>> GetAllPatientDetailsForPatient(int patientId);
    Task AddPatientDetails(PatientDetails patientDetails);
}
