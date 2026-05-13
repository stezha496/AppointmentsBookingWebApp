using AppointmentBookingProjectWebApi.Enums;
using AppointmentBookingProjectWebApi.Models.DTOs;

namespace AppointmentBookingProjectWebApi.Models.DtoMapping;
// Note: Dto -> Entity Id casting may cause issues
public class BookingMapping
{
    // CreateBookingDto -> Booking
    public static Booking ToCreateBooking(CreateBookingDto dto)
    {
        return new Booking
        {
            ReasonForVisit = dto.ReasonForVisit,
            BookedTimeStart = dto.BookedTimeStart,
            BookedTimeDuration = dto.BookedTimeDuration,
            PhysicianId = dto.PhysicianId,
            PatientId = dto.PatientId,
            Created = DateTime.Now,          // set by server
            Status = BookingStatus.Pending   // set by server
        };
    }

    // BookingDto -> Booking
    public static Booking ToBooking(BookingDto dto)
    {
        return new Booking
        {
            ReasonForVisit = dto.ReasonForVisit,
            BookedTimeStart = dto.BookedTimeStart,
            BookedTimeDuration = dto.BookedTimeDuration,
            PhysicianId = dto.PhysicianId,
            PatientId = dto.PatientId,
            Created = DateTime.Now,          // set by server
            Status = BookingStatus.Pending   // set by server
        };
    }

    // Booking -> BookingDto 
    public static BookingDto ToBookingDto(Booking booking)
    {
        return new BookingDto
        {
            BookingId = booking.Id,
            ReasonForVisit = booking.ReasonForVisit ?? string.Empty,
            BookedTimeStart = booking.BookedTimeStart ?? DateTime.MinValue,
            BookedTimeDuration = booking.BookedTimeDuration ?? 0,
            PhysicianId = booking.PhysicianId ?? 0,
            PatientId = booking.PatientId ?? 0,
            PatientName = booking.Patient?.Name,
            Created = booking.Created,
            Status = booking.Status.ToString()
        };
    }

    // List<BookingDto> -> List<CreateBooking>
    public static List<Booking> ToBookingList(List<CreateBookingDto> dtos)
    {
        return dtos.Select(dto => ToCreateBooking(dto)).ToList();
    }

    // List<Booking> -> List<BookingDto>
    public static List<BookingDto> ToBookingDtoList(List<Booking> bookings)
    {
        return bookings.Select(booking => ToBookingDto(booking)).ToList();
    }
}
