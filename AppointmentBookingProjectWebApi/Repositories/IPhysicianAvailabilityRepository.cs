using AppointmentBookingProjectWebApi.Models;

namespace AppointmentBookingProjectWebApi.Repositories;

public interface IPhysicianAvailabilityRepository
{
    Task<List<PhysicianAvailability>> GetAllPhysicianAvailability();
    Task<List<PhysicianAvailability>> GetAllTimeSlotsByPhysician(int physicianId);
    Task<List<PhysicianAvailability>> GetAllAvailabilitiesByPhysician(int physicianId);

    Task SetAvailabilityBooked(int availabilityId);

}
