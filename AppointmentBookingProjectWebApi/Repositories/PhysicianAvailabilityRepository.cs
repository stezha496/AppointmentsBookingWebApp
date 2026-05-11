using AppointmentBookingProjectWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingProjectWebApi.Repositories;

public class PhysicianAvailabilityRepository(AppDbContext context) : IPhysicianAvailabilityRepository
{
    public async Task<List<PhysicianAvailability>> GetAllPhysicianAvailability()
    {
        return await context.PhysicianAvailabilities.ToListAsync();
    }
}
