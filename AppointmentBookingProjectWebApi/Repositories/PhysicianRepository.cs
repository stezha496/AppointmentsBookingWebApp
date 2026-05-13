using AppointmentBookingProjectWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingProjectWebApi.Repositories;

public class PhysicianRepository(AppDbContext context) : IPhysicianRepository
{
    public async Task<List<Physician>> GetAllPhysicians()
    {
        return await context.Physicians.ToListAsync();
    }

    public async Task<int?> GetPhysicianIdByUsername(string username)
    {
        return await context.Physicians
            .Where(x => x.Username == username)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<Physician?> GetPhysicianById(int physicianId)
    {
        return await context.Physicians
            .Where(x => x.Id == physicianId)
            .FirstOrDefaultAsync();
    }
}
