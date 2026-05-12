using AppointmentBookingProjectWebApi.Models.DTOs;
using AppointmentBookingProjectWebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingProjectWebApi.Controllers;

/**
 * Features managed:
 * Login
 * 
 */

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AppUserController : ControllerBase
{
    // Inject repository
    private readonly IAppUserRepository _userRepository;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AppUserController(
        IAppUserRepository userRepository,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager
        )
    {
        _userRepository = userRepository;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        List<IdentityUser> allUsers = await _userRepository.GetAllUsers();
        return Ok(allUsers);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        IdentityUser? user = await _userManager.FindByNameAsync(dto.UserName!);

        if (user == null)
        {
            return Unauthorized("Invalid username or password.");
        }

        Microsoft.AspNetCore.Identity.SignInResult result = 
            await _signInManager.PasswordSignInAsync(user, dto.Password!, false, false);

        if (!result.Succeeded)
        {
            return Unauthorized("Invalid username or password.");
        }

        return Ok(user);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Logged out successfully.");
    }
}
