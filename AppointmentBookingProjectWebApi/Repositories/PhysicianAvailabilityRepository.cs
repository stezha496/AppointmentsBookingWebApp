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

    // Get all available time slots for a specific physician that is later then right now.
    public async Task<List<PhysicianAvailability>> GetAllTimeSlotsByPhysician(int physicianId)
    {
        return await context.PhysicianAvailabilities
            .Where(a => a.PhysicianId == physicianId && a.StartTime > DateTime.Now)
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
