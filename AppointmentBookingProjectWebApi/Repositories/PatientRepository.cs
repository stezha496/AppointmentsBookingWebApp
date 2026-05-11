using AppointmentBookingProjectWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingProjectWebApi.Repositories;

public class PatientRepository(AppDbContext context) : IPatientRepository
{
    public async Task<List<Patient>> GetAllPatients()
    {
        return await context.Patients.ToListAsync();
    }
}
