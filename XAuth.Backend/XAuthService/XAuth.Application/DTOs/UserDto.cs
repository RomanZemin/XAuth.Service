namespace XAuth.Application.DTOs;

public record UserDto(
    string? UserId,
    string? UserName,
    string? Email,
    bool? EmailConfirmed,
    string? PhoneNumber,
    bool? PhoneNumberConfirmed,
    string? JwtToken,
    string? ExpiresAt
    );