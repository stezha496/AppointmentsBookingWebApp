using Microsoft.AspNetCore.Identity;

namespace AppointmentBookingProjectWebApi.Repositories;

// For Identity
public interface IAppUserRepository
{
    Task<List<IdentityUser>> GetAllUsers();
}