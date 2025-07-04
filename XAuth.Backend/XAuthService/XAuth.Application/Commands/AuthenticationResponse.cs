using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using XAuth.Application.DTOs;
using XAuth.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace XAuth.Application.Commands;

/// <summary>
/// DTO для ответа на запрос аутентификации.
/// </summary>
public class AuthenticationResponse
{
    /// <summary>
    /// Флаг, отвечающий за успех выполнения операции.
    /// </summary>
    [JsonPropertyName("succeeded")]
    public bool Succeeded { get; set; }

    /// <summary>
    /// Словарь ошибок, возникших при аутентификации.
    /// </summary>
    [JsonPropertyName("errors")]
    public Dictionary<string, string>? Errors { get; set; }

    /// <summary>
    /// Данные пользователя.
    /// </summary>
    [JsonPropertyName("data")]
    public UserData? Data { get; set; }

    /// <summary>
    /// Данные acess-token.
    /// </summary>
    [JsonPropertyName("access")]
    public AccessTokenDto? Access { get; set; }
}


