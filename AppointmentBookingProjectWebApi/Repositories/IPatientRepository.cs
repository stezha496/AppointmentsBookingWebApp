using AppointmentBookingProjectWebApi.Models;

namespace AppointmentBookingProjectWebApi.Repositories;

public interface IPatientRepository
{
    Task<List<Patient>> GetAllPatients();
}
