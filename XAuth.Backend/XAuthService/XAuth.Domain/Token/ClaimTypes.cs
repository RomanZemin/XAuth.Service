namespace XAuth.Domain.Token;

/// <summary>
/// Статический класс, содержащий (claims), используемых в токенах безопасности.
/// </summary>
public static class ClaimTypes
{
    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public const string UserId = "userId";

    /// <summary>
    /// Электронная почта пользователя.
    /// </summary>
    public const string Email = "email";

    /// <summary>
    /// Уникальный идентификатор токена (JWT ID).
    /// </summary>
    public const string Jti = "jti";

    /// <summary>
    /// Время выпуска токена (Issued At).
    /// </summary>
    public const string Iat = "iat";
}