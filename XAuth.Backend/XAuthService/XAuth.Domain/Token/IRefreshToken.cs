namespace XAuth.Domain.Token;

/// <summary>
/// Интерфейс для работы с refresh token.
/// </summary>
public interface IRefreshToken
{
    /// <summary>
    /// Генерирует и обновляет refresh token.
    /// </summary>
    string? Token { get; }
}