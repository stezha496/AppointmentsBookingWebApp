using AppointmentBookingProjectWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingProjectWebApi.Repositories;

public class PhysicianAvailabilityRepository(AppDbContext context) : IPhysicianAvailabilityRepository
{
    // Only get if available
    public async Task<List<PhysicianAvailability>> GetAllAvailabilitiesByPhysician(int physicianId)
    {
        return await context.PhysicianAvailabilities
            .Where(a => a.PhysicianId == physicianId && a.IsAvailable == true)
            .ToListAsync();
    }
    
    public async Task<List<PhysicianAvailability>> GetAllPhysicianAvailability()
    {
        return await context.PhysicianAvailabilities.ToListAsync();
    }

    public async Task<List<PhysicianAvailability>> GetAllTimeSlotsByPhysician(int physicianId)
    {
        return await context.PhysicianAvailabilities
            .Where(a => a.PhysicianId == physicianId)
            .ToListAsync();
    }

    public async Task SetAvailabilityBooked(int availabilityId)
    {
        PhysicianAvailability? availability = await context.PhysicianAvailabilities
            .FindAsync(availabilityId);

        if (availability != null)
        {
            availability.IsAvailable = false;
            await context.SaveChangesAsync();
        }
    }
}
