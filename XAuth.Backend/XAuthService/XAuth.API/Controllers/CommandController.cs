using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XAuth.Application.Interfaces.IServices;
using XAuth.Identity.Interfaces.IServices;

namespace XAuth.API.Controllers;

[ApiController]
[Route("api/user/commands")]
[Authorize]
public class CommandController : ControllerBase
{
    private readonly ICommandService _commandService;

    public CommandController(ICommandService commandService)
    {
        _commandService = commandService;
    }

    [HttpPost("change-email")]
    public async Task<IActionResult> ChangeEmail([FromQuery] string email, [FromQuery] string code)
    {
        var result = await _commandService.ChangeEmailAsync(User, email, code);
        return result == null ? NotFound("User not found") : Ok(result);
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromQuery] string oldPassword, [FromQuery] string newPassword)
    {
        var result = await _commandService.ChangePasswordAsync(User, oldPassword, newPassword);
        return result == null ? NotFound("User not found") : Ok(result);
    }

    [HttpPost("set-phone-number")]
    public async Task<IActionResult> SetPhoneNumber([FromQuery] string phoneNumber)
    {
        var result = await _commandService.SetPhoneNumberAsync(User, phoneNumber);
        return result == null ? NotFound("User not found") : Ok(result);
    }

    [HttpPost("add-password")]
    public async Task<IActionResult> AddPassword([FromQuery] string newPassword)
    {
        var result = await _commandService.AddPasswordAsync(User, newPassword);
        return result == null ? NotFound("User not found") : Ok(result);
    }
}