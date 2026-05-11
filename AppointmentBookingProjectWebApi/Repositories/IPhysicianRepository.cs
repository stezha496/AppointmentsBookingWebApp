using AppointmentBookingProjectWebApi.Models;

namespace AppointmentBookingProjectWebApi.Repositories;

public interface IPhysicianRepository
{
    Task<List<Physician>> GetAllPhysicians();
}