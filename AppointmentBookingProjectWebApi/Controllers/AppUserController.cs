using AppointmentBookingProjectWebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingProjectWebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AppUserController : ControllerBase
{
    // Inject repository
    private readonly IAppUserRepository _userRepository;
    private UserManager<IdentityUser> userManager;

    public AppUserController
        (
        IAppUserRepository userRepository,
        UserManager<IdentityUser> userManager
        )
    {
        _userRepository = userRepository;
        this.userManager = userManager;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllUsers()
    {
        List<IdentityUser> allUsers = await _userRepository.GetAllUsers();
        return Ok(allUsers);
    }
}
