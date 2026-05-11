using AppointmentBookingProjectWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingProjectWebApi.Repositories;

public class BookingRepository(AppDbContext context) : IBookingRepository
{
    public async Task<List<Booking>> GetAllBookings()
    {
        return await context.Bookings.ToListAsync();
    }
}
