using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XAuth.Application.Interfaces.IServices;

namespace XAuth.API.Controllers;

[ApiController]
[Route("api/user/queries")]
[Authorize]
public class UserQueryController : ControllerBase
{
    private readonly IQueryService _queryService;

    public UserQueryController(IQueryService queryService)
    {
        _queryService = queryService;
    }

    [HttpGet("by-id")]
    public async Task<IActionResult> GetById([FromQuery] string userId)
    {
        var user = await _queryService.FindByIdAsync(userId);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpGet("by-email")]
    public async Task<IActionResult> GetByEmail([FromQuery] string email)
    {
        var user = await _queryService.FindByEmailAsync(email);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetUserId()
    {
        var userId = await _queryService.GetUserIdAsync(User);
        return Ok(userId);
    }

    [HttpGet("email-confirmed")]
    public async Task<IActionResult> IsEmailConfirmed([FromQuery] string email)
    {
        var confirmed = await _queryService.IsEmailConfirmedAsync(email);
        return Ok(confirmed);
    }

    [HttpGet("has-password")]
    public async Task<IActionResult> HasPassword()
    {
        var hasPassword = await _queryService.HasPasswordAsync(User);
        return Ok(hasPassword);
    }

    [HttpGet("email")]
    public async Task<IActionResult> GetEmail()
    {
        var email = await _queryService.GetEmailAsync(User);
        return Ok(email);
    }

    [HttpGet("username")]
    public async Task<IActionResult> GetUserName()
    {
        var username = await _queryService.GetUserNameAsync(User);
        return Ok(username);
    }

    [HttpGet("phone-number")]
    public async Task<IActionResult> GetPhoneNumber()
    {
        var phone = await _queryService.GetPhoneNumberAsync(User);
        return Ok(phone);
    }
}