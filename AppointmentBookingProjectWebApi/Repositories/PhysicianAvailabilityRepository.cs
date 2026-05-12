using AppointmentBookingProjectWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingProjectWebApi.Repositories;

public class PhysicianAvailabilityRepository(AppDbContext context) : IPhysicianAvailabilityRepository
{
    public async Task<List<PhysicianAvailability>> GetAllPhysicianAvailability()
    {
        return await context.PhysicianAvailabilities.ToListAsync();
    }

    public async Task<List<PhysicianAvailability>> GetAvailabilitiesByPhysician(int physicianId)
    {
        return await context.PhysicianAvailabilities
            .Where(a => a.PhysicianId == physicianId)
            .ToListAsync();
    }
}
