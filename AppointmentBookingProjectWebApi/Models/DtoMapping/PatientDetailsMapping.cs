using AppointmentBookingProjectWebApi.Models.DTOs;

namespace AppointmentBookingProjectWebApi.Models.DtoMapping;

public class PatientDetailsMapping
{
    // PatientDetailsDto -> PatientDetails
    public static PatientDetails ToPatientDetails(PatientDetailsDto dto)
    {
        return new PatientDetails
        {
            Age = dto.Age,
            Gender = dto.Gender,
            Height = dto.Height,
            Weight = dto.Weight,
            PhoneNumber = dto.PhoneNumber,
            patientId = dto.patientId,
            //Created = DateTime.Now,
        };
    }

    // PatientDetails -> PatientDetailsDto
    public static PatientDetailsDto ToPatientDetailsDto(PatientDetails patientDetails)
    {
        return new PatientDetailsDto
        {
            Age = patientDetails.Age ?? 0,
            Gender = patientDetails.Gender ?? string.Empty,
            Height = patientDetails.Height ?? 0,
            Weight = patientDetails.Weight ?? 0,
            PhoneNumber = patientDetails.PhoneNumber ?? string.Empty,
            patientId = patientDetails.patientId ?? 0,
        };
    }

    // List<PatientDetailsDto> -> List<PatientDetails>
    public static List<PatientDetails> ToPatientDetailsList(List<PatientDetailsDto> dtos)
    {
        return dtos.Select(dto => ToPatientDetails(dto)).ToList();
    }

    // List<PatientDetails> -> List<PatientDetailsDto>
    public static List<PatientDetailsDto> ToPatientDetailsDtoList(List<PatientDetails> patientDetailsList)
    {
        return patientDetailsList.Select(pd => ToPatientDetailsDto(pd)).ToList();
    }
}