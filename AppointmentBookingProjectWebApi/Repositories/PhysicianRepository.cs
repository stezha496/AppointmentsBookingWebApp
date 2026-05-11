using AppointmentBookingProjectWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingProjectWebApi.Repositories;

public class PhysicianRepository(AppDbContext context) : IPhysicianRepository
{
    public async Task<List<Physician>> GetAllPhysicians()
    {
        return await context.Physicians.ToListAsync();
    }
}
