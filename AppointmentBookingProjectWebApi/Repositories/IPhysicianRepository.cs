using AppointmentBookingProjectWebApi.Models;

namespace AppointmentBookingProjectWebApi.Repositories;

public interface IPhysicianRepository
{
    Task<List<Physician>> GetAllPhysicians();
    Task<Physician?> GetPhysicianById(int physicianId);
    Task<int?> GetPhysicianIdByUsername(string username);

}