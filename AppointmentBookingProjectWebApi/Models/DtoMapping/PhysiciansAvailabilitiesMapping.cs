using AppointmentBookingProjectWebApi.Models;
using AppointmentBookingProjectWebApi.Models.DTOs;

namespace AppointmentBookingProjectWebApi.Mappings;

// Note: Dto -> Entity Id casting may cause issues
public static class PhysiciansAvailabilitiesMapping
{
    // Entity -> DTO
    public static PhysicianAvailabilityDto ToDto(this PhysicianAvailability availability)
    {
        return new PhysicianAvailabilityDto
        {
            Id = availability.Id,
            PhysicianId = availability.PhysicianId,
            PhysicianName = availability.Physician?.Name ?? string.Empty,
            StartTime = availability.StartTime,
            EndTime = availability.EndTime,
            IsAvailable = availability.IsAvailable
        };
    }

    // DTO -> Entity
    public static PhysicianAvailability ToEntity(this PhysicianAvailabilityDto dto)
    {
        return new PhysicianAvailability
        {
            Id = (int)dto.Id,
            PhysicianId = dto.PhysicianId,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            IsAvailable = dto.IsAvailable
        };
    }

    // Entity List -> DTO List
    public static List<PhysicianAvailabilityDto> ToDtoList(
        this IEnumerable<PhysicianAvailability> availabilities)
    {
        return availabilities
            .Select(a => a.ToDto())
            .ToList();
    }

    // DTO List -> Entity List
    public static List<PhysicianAvailability> ToEntityList(
        this IEnumerable<PhysicianAvailabilityDto> dtos)
    {
        return dtos
            .Select(d => d.ToEntity())
            .ToList();
    }
}