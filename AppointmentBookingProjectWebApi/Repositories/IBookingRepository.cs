using AppointmentBookingProjectWebApi.Models;

namespace AppointmentBookingProjectWebApi.Repositories;

public interface IBookingRepository
{
    Task<List<Booking>> GetAllBookings();
    Task<List<Booking>> GetBookingsByPatient(int patientId);
    Task<List<Booking>> GetBookingsByPhysician(int physicianId);

    Task<List<Booking>> GetBookingsByPatientUsername(string patientUsername);
    Task<List<Booking>> GetBookingsByPhysicianUsername(string physicianUsername);

    Task CreateBooking(Booking newBooking);
}