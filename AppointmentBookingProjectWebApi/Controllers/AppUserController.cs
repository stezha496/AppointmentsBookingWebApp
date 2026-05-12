using AppointmentBookingProjectWebApi.Models.DTOs;
using AppointmentBookingProjectWebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AppointmentBookingProjectWebApi.Controllers;

/**
 * Features managed:
 * - Login
 * - Logout
 * - Get user info (indentity)
 */

//[Authorize]
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

    [HttpGet("all-users")]
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

        IList<string> roles = await _userManager.GetRolesAsync(user);

        // Just return what the frontend needs
        return Ok(new
        {
            username = user.UserName,
            email = user.Email,
            role = roles.First()
        });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Logged out successfully.");
    }

    // Get currently logged in user
    [HttpGet("current-user")]
    public async Task<IActionResult> GetCurrentUser()
    {
        IdentityUser? currentUser = await _userManager.GetUserAsync(User);

        if (currentUser == null)
        {
            return Unauthorized();
        }

        return Ok(currentUser);
    }

    // Gets user role (identity)
    [HttpGet("{id}/role")]
    public async Task<IActionResult> GetUserRole(string id)
    {
        IdentityUser? user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        IList<string> roles = await _userManager.GetRolesAsync(user);
        string role = roles.FirstOrDefault();

        if (role == null)
        {
            return NotFound("No role assigned to this user.");
        }

        //Console.WriteLine($"Role for user {id}: {role}");
        return Ok(role);
    }
}
