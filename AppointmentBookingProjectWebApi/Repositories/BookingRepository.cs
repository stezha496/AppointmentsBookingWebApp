using AppointmentBookingProjectWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingProjectWebApi.Repositories;

public class BookingRepository(AppDbContext context) : IBookingRepository
{
    public async Task<List<Booking>> GetAllBookings()
    {
        return await context.Bookings
        .Include(b => b.Physician)
        .Include(b => b.Patient)
        .ToListAsync();
    }

    public async Task<List<Booking>> GetBookingsByPatient(int patientId)
    {
        return await context.Bookings
            .Where(b => b.PatientId == patientId)
            .Include(b => b.Physician)
            .Include(b => b.Patient)
            .ToListAsync();
    }

    public async Task<List<Booking>> GetBookingsByPhysician(int physicianId)
    {
        return await context.Bookings
            .Where(b => b.PhysicianId == physicianId)
            .Include(b => b.Physician)
            .Include(b => b.Patient)
            .ToListAsync();
    }

    public async Task CreateBooking(Booking newBooking)
    {
        await context.Bookings.AddAsync(newBooking);
        await context.SaveChangesAsync();
    }
}
