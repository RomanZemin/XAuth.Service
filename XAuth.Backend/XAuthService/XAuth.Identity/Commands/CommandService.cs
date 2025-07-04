using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using XAuth.Application.Commands;
using XAuth.Application.DTOs;
using XAuth.Application.Extensions;
using XAuth.Application.Interfaces.IServices;
using XAuth.Domain.Entities;
using XAuth.Identity.Interfaces;
using XAuth.Identity.Interfaces.IServices;

namespace XAuth.Identity.Commands;

public class CommandService : ICommandService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;

    public CommandService(UserManager<User> userManager,
        IMapper mapper)
    {
        _userManager = userManager;
    }

    public async Task<AuthenticationResponse?> ChangeEmailAsync(ClaimsPrincipal principal,
        string email,
        string code,
        SignInManager<User> signInManager)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user == null) return null;

        var result = await _userManager.ChangeEmailAsync(user, email, code);
        return IdentityResultExtensions.ToAuthenticationResponse(result, null, user);
    }

    public async Task<AuthenticationResponse?> ChangePasswordAsync(ClaimsPrincipal principal, string oldPassword, string newPassword)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user == null) return null;

        var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        return IdentityResultExtensions.ToAuthenticationResponse(result, null, user);
    }

    public async Task<AuthenticationResponse?> SetPhoneNumberAsync(ClaimsPrincipal principal, string phoneNumber)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user == null) return null;

        var result = await _userManager.SetPhoneNumberAsync(user, phoneNumber);
        return IdentityResultExtensions.ToAuthenticationResponse(result, null, user);
    }

    public async Task<AuthenticationResponse?> AddPasswordAsync(ClaimsPrincipal principal, string newPassword)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user == null) return null;

        var result = await _userManager.AddPasswordAsync(user, newPassword);
        return IdentityResultExtensions.ToAuthenticationResponse(result, null, user);
    }
    
    /// <summary>
    /// Метод для выполнения аутентификации пользователя с помощью логина и пароля.
    /// </summary>
    /// <param name="request">Запрос для входа с email, паролем и флагом запоминания пользователя.</param>
    /// <returns>Ответ аутентификации с JWT токеном.</returns>
    public async Task<AuthenticationResponse> SignInAsync(SignInRequest signInRequest)
    {
        SignInResult signInResult = await _signInManager.PasswordSignInAsync(signInRequest.Email, signInRequest.Password, signInRequest.RememberMe, false);

        User? user = await _userManager.FindByEmailAsync(signInRequest.Email);
        var userDto = _mapper.Map<UserDto>(user);
        if (user != null)
        {
            user.JwtToken = _jwtService.GenerateJwtToken(_mapper.Map<User, UserDto>(user));
            user.ExpiresAt = DateTime.UtcNow.AddHours(24).ToString("O");
        }

        var identityResult = IdentityResult.Success;

        return identityResult.ToAuthenticationResponse(signInResult, user);
    }
    
    /// <summary>
    /// Метод для изменения email пользователя.
    /// </summary>
    /// <param name="principal">Объект, содержащий информацию о текущем пользователе.</param>
    /// <param name="email">Новый email.</param>
    /// <param name="code">Код подтверждения для изменения email.</param>
    /// <returns>Ответ с результатом операции изменения email.</returns>
    public async Task<AuthenticationResponse?> ChangeEmailAsync(ClaimsPrincipal principal, string email, string code)
    {
        User? user = await _userManager.GetUserAsync(principal);
        if (user == null)
        {
            return null;
        }
        IdentityResult? result = await _userManager.ChangeEmailAsync(user, email, code);
        return result.ToAuthenticationResponse(null, user);
    }
    
}