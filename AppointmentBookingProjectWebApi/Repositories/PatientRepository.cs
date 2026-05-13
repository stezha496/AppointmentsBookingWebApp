using AppointmentBookingProjectWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingProjectWebApi.Repositories;

public class PatientRepository(AppDbContext context) : IPatientRepository
{
    public async Task<List<Patient>> GetAllPatients()
    {
        return await context.Patients.ToListAsync();
    }

    public async Task<Patient?> GetPatientById(int patientId)
    {
        return await context.Patients
            .Where(x => x.Id  == patientId)
            .FirstOrDefaultAsync();
    }

    public async Task<int?> GetPatientIdByUsername(string username)
    {
        return await context.Patients
            .Where(x => x.Username == username)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
    }
}
