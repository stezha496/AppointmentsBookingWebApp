using AppointmentBookingProjectWebApi.Models;

namespace AppointmentBookingProjectWebApi.Repositories;

public interface IBookingRepository
{
    Task<List<Booking>> GetAllBookings();
}
