using System.Collections.Concurrent;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using XAuth.Application.DTOs;
using XAuth.Domain.Entities;
using XAuth.Domain.Token;
using XAuth.Identity.Interfaces;
using XAuth.Identity.Jwt.Helpers;
using ClaimTypes = System.Security.Claims.ClaimTypes;

namespace XAuth.Identity.Jwt.Services;

/// <summary>
/// Класс <c>JWTService</c> реализует интерфейс <c>IJWTService</c> и предоставляет функциональность для работы с JWT и refresh токенами.
/// Включает методы для генерации JWT, проверки токенов, а также для работы с refresh токенами.
/// </summary>
public partial class JwtService: IJwtService
{

    private readonly JwtEncryptionHelper _jwtServiceHelper;
    
    /// <summary>
    /// Секретный ключ, используемый для подписи JWT и refresh токенов.
    /// </summary>
    private readonly string _secretKey;

    /// <summary>
    /// (issuer) токенов.
    /// </summary>
    private readonly string _issuer;

    /// <summary>
    /// (audience) токенов.
    /// </summary>
    private readonly string _audience;

    /// <summary>
    /// ConcurrentDictionary для хранения refresh токенов.
    /// </summary>
    private static readonly ConcurrentDictionary<string, RefreshToken> _refreshTokens = new ConcurrentDictionary<string, RefreshToken>();

    /// <summary>
    /// Конструктор класса <c>JWTService (_secretKey = secretKey, _issuer = issuer, _audience = audience)</c>.
    /// </summary>
    /// <param name="secretKey">Секретный ключ для подписания токенов.</param>
    /// <param name="issuer">(issuer) токенов.</param>
    /// <param name="audience">(audience) токенов.</param>
    public JwtService(string secretKey, string issuer, string audience, JwtEncryptionHelper jwtServiceHelper)
    {
        _secretKey = secretKey;
        _issuer = issuer;
        _audience = audience;
        _jwtServiceHelper = jwtServiceHelper;
    }
    
    /// <summary>
    /// Генерирует JWT-токен для указанного пользователя.
    /// </summary>
    /// <param name="user">Объект <see cref="ApplicationUserDto"/>, содержащий данные пользователя для генерации токена.</param>
    /// <returns>
    /// Возвращает строку, представляющую JWT-токен.
    /// </returns>
    public string GenerateJwtToken(UserDto user)
    {
        // Создаем список утверждений (claims) для токена
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId ?? string.Empty),
            new Claim(Domain.Token.ClaimTypes.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty)
        };
        
        var payload = new Dictionary<string, object>
        {
            { "iss", _issuer },
            { "aud", _audience },
            { "exp", DateTimeOffset.UtcNow.AddHours(24).ToUnixTimeSeconds() },
            { "iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds() },
            { "sub", user.UserId ?? string.Empty },
            { "name", user.UserName ?? string.Empty },
            { Domain.Token.ClaimTypes.Jti, Guid.NewGuid().ToString() }
        };

        // Сериализуем заголовок и полезную нагрузку в формате JSON
        var headerJson = JsonSerializer.Serialize(new { alg = "HS256", typ = "JWT" });
        var payloadJson = JsonSerializer.Serialize(payload);

        // Кодирует header и payload в Base64Url
        var headerEncoded = _jwtServiceHelper.Base64UrlEncode(Encoding.UTF8.GetBytes(headerJson));
        var payloadEncoded = _jwtServiceHelper.Base64UrlEncode(Encoding.UTF8.GetBytes(payloadJson));

        // Вычисляем подпись токена
        var signature = _jwtServiceHelper.ComputeHmacSha256($"{headerEncoded}.{payloadEncoded}", Encoding.UTF8.GetBytes(_secretKey));
        var signatureEncoded = _jwtServiceHelper.Base64UrlEncode(signature);

        // Возвращаем итоговый JWT-токен
        return $"{headerEncoded}.{payloadEncoded}.{signatureEncoded}";
    }
    
    /// <summary>
    /// Генерирует новый Refresh токен.
    /// </summary>
    /// <returns>
    /// Возвращает строку, представляющую новый Refresh токен.
    /// </returns>
    public string GenerateRefreshToken()
    {
        // Генерация нового уникального Refresh токена
        var refreshToken = Guid.NewGuid().ToString();
        var refreshTokenObject = new RefreshToken(refreshToken);
        _refreshTokens[refreshToken] = refreshTokenObject;
        return refreshToken;
    }
    
    /// <summary>
    /// Обновляет JWT-токен с использованием предоставленного Refresh токена.
    /// </summary>
    /// <param name="refreshToken">Refresh токен, используемый для получения нового JWT-токена.</param>
    /// <param name="user">Объект <see cref="ApplicationUserDto"/>, содержащий данные пользователя.</param>
    /// <returns>
    /// Возвращает объект <see cref="AccessTokenDto"/>, содержащий новый Refresh токен, новый JWT токен и время истечения токена.
    /// </returns>
    /// <exception cref="Domain.Token.SecurityTokenException">
    /// Выбрасывается, если предоставленный Refresh токен не найден.
    /// </exception>
    public AccessTokenDto RefreshToken(string refreshToken, UserDto user)
    {
        if (_refreshTokens.TryGetValue(refreshToken, out var tokenObject))
        {
            _refreshTokens.TryRemove(refreshToken, out _);
            
            var newJwtToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();

            _refreshTokens[newRefreshToken] = new RefreshToken(newRefreshToken);
            return new AccessTokenDto
            {
                Refresh_Token = newRefreshToken,
                Jwt = newJwtToken,
                ExpiresAt = DateTime.UtcNow.AddHours(24).ToString()
            };
        }
        else
        {
            throw new SecurityTokenException("Invalid refresh token");
        }
    }
    
    /// <summary>
    /// Валидирует переданный JWT токен.
    /// </summary>
    /// <param name="token">JWT токен для валидации.</param>
    /// <returns>
    /// Возвращает <c>true</c>, если токен валиден, и <c>false</c> в противном случае.
    /// </returns>
    public bool ValidateToken(string token)
    {
        // Разделяем токен на части (header, payload, signature)
        var parts = token.Split('.');

        if (parts.Length != 3)
            return false;

        var header = parts[0];
        var payload = parts[1];
        var signature = parts[2];

        // Вычисляем подпись и проверяем ее
        var key = Encoding.UTF8.GetBytes(_secretKey);
        var computedSignature = _jwtServiceHelper.ComputeHmacSha256($"{header}.{payload}", key);
        var computedSignatureEncoded = _jwtServiceHelper.Base64UrlEncode(computedSignature);

        // Если подписи не совпадают, токен недействителен
        if (computedSignatureEncoded != signature)
            return false;

        // Десериализуем payload и проверяем срок годности
        var payloadJson = Encoding.UTF8.GetString(_jwtServiceHelper.Base64UrlDecode(payload));
        var payloadData = JsonSerializer.Deserialize<Dictionary<string, object>>(payloadJson);

        if (payloadData == null || !payloadData.TryGetValue("exp", out var exp) || !long.TryParse(exp.ToString(), out var expSeconds))
            return false;

        var expirationTime = DateTimeOffset.FromUnixTimeSeconds(expSeconds);

        return expirationTime > DateTimeOffset.UtcNow;
    }
    
}
