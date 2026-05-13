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

    public async Task<List<Booking>> GetBookingsByPatientUsername(string patientUsername)
    {
        // First find the patientId related to the username
        int patientId = await context.Patients
            .Where(x => x.Username == patientUsername)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        return await context.Bookings
            .Where(x => x.PatientId == patientId)
            .Include(b => b.Physician)
            .Include(b => b.Patient)
            .ToListAsync();
    }

    public async Task<List<Booking>> GetBookingsByPhysicianUsername(string physicianUsername)
    {
        // First find the patientId related to the username
        int physicianId = await context.Physicians
            .Where(x => x.Username == physicianUsername)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        return await context.Bookings
            .Where(x => x.PatientId == physicianId)
            .Include(b => b.Physician)
            .Include(b => b.Patient)
            .ToListAsync();
    }

    public async Task<List<Booking>> GetUpcomingBookingsByPhysician(int physicianId)
    {
        return await context.Bookings
            .Where(x => physicianId == x.PhysicianId && x.BookedTimeStart > DateTime.Now)
            .Include(b => b.Physician)
            .Include(b => b.Patient)
            .ToListAsync();
    }
}
