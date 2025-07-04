using System.Text.Json.Serialization;

namespace XAuth.Application.DTOs;

/// <summary>
/// DTO для передачи информации о токене доступа.
/// </summary>
public class AccessTokenDto
{
    /// <summary>
    /// Обновленный токен.
    /// </summary>
    [JsonPropertyName("Refresh_Token")]
    public string? Refresh_Token { get; set; }

    /// <summary>
    /// JWT токен.
    /// </summary>
    [JsonPropertyName("jwt")]
    public string? Jwt { get; set; }

    /// <summary>
    /// Дата и время истечения срока действия токена.
    /// </summary>
    [JsonPropertyName("expiresAt")]
    public string? ExpiresAt { get; set; }
}