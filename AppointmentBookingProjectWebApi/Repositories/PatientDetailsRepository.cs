using AppointmentBookingProjectWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingProjectWebApi.Repositories;

public class PatientDetailsRepository(AppDbContext context) : IPatientDetailsRepository
{
    public async Task<List<PatientDetails>> GetAllPatientDetails()
    {
        return await context.PatientDetails
            .Include(x => x.Patient)
            .ToListAsync();
    }

    public async Task<List<PatientDetails>> GetAllPatientDetailsForPatient(int patientId)
    {
        return await context.PatientDetails
            .Where(x => x.patientId == patientId)
            .Include(x => x.Patient)
            .ToListAsync();
    }

    // Will check in the front end if the patient details are valid.
    public async Task AddPatientDetails(PatientDetails patientDetails)
    {
        await context.PatientDetails.AddAsync(patientDetails);
        await context.SaveChangesAsync();
    }
}
