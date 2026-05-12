using AppointmentBookingProjectWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingProjectWebApi.Repositories;

public class BookingRepository(AppDbContext context) : IBookingRepository
{
    public async Task<List<Booking>> GetAllBookings()
    {
        return await context.Bookings.ToListAsync();
    }

    public async Task<List<Booking>> GetBookingsByPatient(int patientId)
    {
        return await context.Bookings
            .Where(b => b.PatientId == patientId)
            .ToListAsync();
    }

    public async Task<List<Booking>> GetBookingsByPhysician(int physicianId)
    {
        return await context.Bookings
            .Where(b => b.PhysicianId == physicianId)
            .ToListAsync();
    }
}
