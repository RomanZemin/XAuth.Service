using System.Security.Claims;
using XAuth.Application.Commands;

namespace XAuth.Identity.Interfaces.IServices;

public interface ICommandService
{
    Task<AuthenticationResponse?> ChangeEmailAsync(ClaimsPrincipal principal, string email, string code);
    Task<AuthenticationResponse?> ChangePasswordAsync(ClaimsPrincipal principal, string oldPassword, string newPassword);
    Task<AuthenticationResponse?> SetPhoneNumberAsync(ClaimsPrincipal principal, string phoneNumber);
    Task<AuthenticationResponse?> AddPasswordAsync(ClaimsPrincipal principal, string newPassword);

    Task<AuthenticationResponse> SignInAsync(SignInRequest signInRequest);
}