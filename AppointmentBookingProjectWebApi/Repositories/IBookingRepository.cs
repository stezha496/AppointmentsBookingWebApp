using AppointmentBookingProjectWebApi.Models;

namespace AppointmentBookingProjectWebApi.Repositories;

public interface IBookingRepository
{
    Task<List<Booking>> GetAllBookings();
    Task<List<Booking>> GetBookingsByPatient(int patientId);
    Task<List<Booking>> GetBookingsByPhysician(int physicianId);
}