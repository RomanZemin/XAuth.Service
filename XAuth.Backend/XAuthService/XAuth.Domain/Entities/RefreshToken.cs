using XAuth.Domain.Token;

namespace XAuth.Domain.Entities;

public record RefreshToken(string? Token) : IRefreshToken;
