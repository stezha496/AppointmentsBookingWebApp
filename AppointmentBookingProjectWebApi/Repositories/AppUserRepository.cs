using AppointmentBookingProjectWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingProjectWebApi.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly AppDbContext context;
    private readonly UserManager<IdentityUser> userManager;

    public AppUserRepository(AppDbContext context, UserManager<IdentityUser> userManager)
    {
        this.context = context;
        this.userManager = userManager;
    }

    public async Task<List<IdentityUser>> GetAllUsers()
    {
        return await context.Users.ToListAsync();
    }
}
