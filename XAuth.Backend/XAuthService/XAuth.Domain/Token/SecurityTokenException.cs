namespace XAuth.Domain.Token;

/// <summary>
/// Класс исключений, которые возникают при работе с security-токенами.
/// </summary>
public class SecurityTokenException : Exception
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="SecurityTokenException"/> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение об ошибке, описывающее причину исключения.</param>
    public SecurityTokenException(string message) : base(message) { }
}