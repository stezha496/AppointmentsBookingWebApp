using AppointmentBookingProjectWebApi.Models;
using AppointmentBookingProjectWebApi.Models.DTOs;

namespace AppointmentBookingProjectWebApi.Mappings;

// Note: Dto -> Entity Id casting may cause issues
/// <summary>
/// Maps Physician Entity related DTOs
/// </summary>
public static class PhysicianMapping
{
    // Entity -> DTO
    public static PhysicianDto ToDto(this Physician physician)
    {
        return new PhysicianDto
        {
            PhysicianId = physician.Id,
            Name = physician.Name,
            PhoneNumber = physician.PhoneNumber,
            Username = physician.Username,
            Email = physician.Email
        };
    }

    // DTO -> Entity
    public static Physician ToEntity(this PhysicianDto dto)
    {
        return new Physician
        {
            Id = (int)dto.PhysicianId,
            Name = dto.Name,
            PhoneNumber = dto.PhoneNumber,
            Username = dto.Username,
            Email = dto.Email
        };
    }

    // Entity List -> DTO List
    public static List<PhysicianDto> ToDtoList(
        this IEnumerable<Physician> physicians)
    {
        return physicians
            .Select(p => p.ToDto())
            .ToList();
    }

    // DTO List -> Entity List
    public static List<Physician> ToEntityList(
        this IEnumerable<PhysicianDto> dtos)
    {
        return dtos
            .Select(d => d.ToEntity())
            .ToList();
    }
}