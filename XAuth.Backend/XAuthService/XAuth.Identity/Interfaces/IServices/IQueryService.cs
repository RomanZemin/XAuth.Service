using System.Security.Claims;
using XAuth.Application.DTOs;

namespace XAuth.Application.Interfaces.IServices;

public interface IQueryService
{
    Task<UserDto?> FindByIdAsync(string userId);
    Task<UserDto?> FindByEmailAsync(string email);
    Task<string> GetUserIdAsync(ClaimsPrincipal principal);
    Task<bool> IsEmailConfirmedAsync(string email);
    Task<bool> HasPasswordAsync(ClaimsPrincipal principal);
    Task<string> GetEmailAsync(ClaimsPrincipal principal);
    Task<string> GetUserNameAsync(ClaimsPrincipal principal);
    Task<string> GetPhoneNumberAsync(ClaimsPrincipal principal); 
}