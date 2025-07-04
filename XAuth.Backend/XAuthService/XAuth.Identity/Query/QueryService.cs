using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using XAuth.Application.DTOs;
using XAuth.Application.Interfaces.IServices;
using XAuth.Domain.Entities;

namespace XAuth.Identity.Query;

public class QueryService : IQueryService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public QueryService(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UserDto?> FindByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> FindByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<string> GetUserIdAsync(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);
        string userId = await _userManager.GetUserIdAsync(user);
        return userId;
    }

    public async Task<bool> IsEmailConfirmedAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null && await _userManager.IsEmailConfirmedAsync(user);
    }

    public async Task<bool> HasPasswordAsync(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);
        return user != null && await _userManager.HasPasswordAsync(user);
    }

    public async Task<string> GetEmailAsync(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);
        return user == null ? string.Empty : await _userManager.GetEmailAsync(user) ?? string.Empty;
    }

    public async Task<string> GetUserNameAsync(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);
        return user == null ? string.Empty : await _userManager.GetUserNameAsync(user) ?? string.Empty;
    }

    public async Task<string> GetPhoneNumberAsync(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);
        return user == null ? string.Empty : await _userManager.GetPhoneNumberAsync(user) ?? string.Empty;
    }
}