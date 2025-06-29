using MediatR;
using Microsoft.AspNetCore.Mvc;
using XAuth.Application.Commands;

namespace XAuth.Service.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); ;
    }

    /// <summary>
    /// Регистрация пользователя.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInUserCommand command, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpPost("signout")]
    public async Task<IActionResult> SignOut()
    {
        throw new NotImplementedException();
    }

    [HttpPost("resetpassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// Подтвердить почту.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    [HttpPost("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpGet("whoami")]
    public Task<IActionResult> WhoAmI()
    {
        if (User.Identity != null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Task.FromResult<IActionResult>(Ok(new { User.Identity.Name }));
            }
            return Task.FromResult<IActionResult>(Unauthorized());
        }
        return Task.FromResult<IActionResult>(Unauthorized());
    }
}