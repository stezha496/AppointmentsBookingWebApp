using AppointmentBookingProjectWebApi.Models;

namespace AppointmentBookingProjectWebApi.Repositories;

public interface IPhysicianAvailabilityRepository
{
    Task<List<PhysicianAvailability>> GetAllPhysicianAvailability();
    Task<List<PhysicianAvailability>> GetAvailabilitiesByPhysician(int physicianId);

}
