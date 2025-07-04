using System.Text.Json.Serialization;

namespace XAuth.Application.DTOs;

/// <summary>
/// DTO для данных пользователя.
/// </summary>
public class UserData
{
    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    [JsonPropertyName("userId")]
    public string? UserId { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    [JsonPropertyName("UserName")]
    public string? UserName { get; set; }

    /// <summary>
    /// Имя пользователя (фактическое).
    /// </summary>
    [JsonPropertyName("FirstName")]
    public string? FirstName { get; set; }

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    [JsonPropertyName("LastName")]
    public string? LastName { get; set; }
}