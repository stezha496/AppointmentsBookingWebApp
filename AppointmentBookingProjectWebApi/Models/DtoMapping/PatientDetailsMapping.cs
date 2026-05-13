using AppointmentBookingProjectWebApi.Models.DTOs;

namespace AppointmentBookingProjectWebApi.Models.DtoMapping;

// Note: Dto -> Entity Id casting may cause issues
/// <summary>
/// Maps PatientDetails and PatientDetails Entity related DTOs
/// </summary>
public class PatientDetailsMapping
{
    // PatientDetailsDto -> PatientDetails
    public static PatientDetails ToPatientDetails(PatientDetailsDto dto)
    {
        return new PatientDetails
        {
            Id = (int)dto.PatientDetailsId,
            Age = dto.Age,
            Gender = dto.Gender,
            Height = dto.Height,
            Weight = dto.Weight,
            PhoneNumber = dto.PhoneNumber,
            patientId = dto.PatientId,
            //Created = DateTime.Now,
        };
    }

    // PatientDetails -> PatientDetailsDto
    public static PatientDetailsDto ToPatientDetailsDto(PatientDetails patientDetails)
    {
        return new PatientDetailsDto
        {
            PatientDetailsId = patientDetails.Id,
            Age = patientDetails.Age ?? 0,
            Gender = patientDetails.Gender ?? string.Empty,
            Height = patientDetails.Height ?? 0,
            Weight = patientDetails.Weight ?? 0,
            PhoneNumber = patientDetails.PhoneNumber ?? string.Empty,
            PatientId = patientDetails.patientId ?? 0,
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