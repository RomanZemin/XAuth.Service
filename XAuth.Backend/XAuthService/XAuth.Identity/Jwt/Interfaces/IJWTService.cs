using XAuth.Application.DTOs;

namespace XAuth.Identity.Interfaces;

/// <summary>
/// Интерфейс для работы с JWT-токенами и токенами обновления.
/// Определяет методы генерации, обновления и валидации токенов.
/// </summary>
public interface IJwtService
{
    /// <summary>
    /// Генерирует JWT-токен для указанного пользователя.
    /// </summary>
    /// <param name="user">
    /// Объект пользователя <see cref="ApplicationUserDto"/>, для которого создается токен.
    /// </param>
    /// <returns>
    /// Строка, представляющая сгенерированный JWT-токен.
    /// </returns>
    string GenerateJwtToken(UserDto user);

    /// <summary>
    /// Генерирует новый токен обновления (Refresh Token).
    /// </summary>
    /// <returns>
    /// Строка, представляющая сгенерированный токен обновления.
    /// </returns>
    string GenerateRefreshToken();

    /// <summary>
    /// Обновляет токен доступа (JWT) на основе токена обновления.
    /// </summary>
    /// <param name="refreshToken">
    /// Текущий токен обновления, который нужно проверить и использовать для обновления.
    /// </param>
    /// <param name="user">
    /// Объект пользователя <see cref="ApplicationUserDto"/>, для которого создается новый токен.
    /// </param>
    /// <returns>
    /// Объект <see cref="AccessTokenDto"/>, содержащий новый JWT и обновленный Refresh Token.
    /// </returns>
    AccessTokenDto RefreshToken(string refreshToken, UserDto user);

    /// <summary>
    /// Валидирует переданный JWT-токен.
    /// </summary>
    /// <param name="token">
    /// Строка, представляющая токен, который нужно проверить.
    /// </param>
    /// <returns>
    /// Возвращает true, если токен действителен.
    /// </returns>
    bool ValidateToken(string token);
}