using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using XAuth.Domain.Entities;

namespace XAuth.Identity.Jwt.Helpers;

/// <summary>
/// Класс <c>JWTService</c> содержит вспомогательные методы для работы с криптографией и кодированием/декодированием данных.
/// Эти методы используются для вычисления HMAC, а также для кодирования и декодирования Base64Url данных.
/// </summary>
public class JwtEncryptionHelper
    {
        
        /// <summary>
        /// ConcurrentDictionary для хранения refresh токенов.
        /// </summary>
        private static readonly ConcurrentDictionary<string, RefreshToken> _refreshTokens = new ConcurrentDictionary<string, RefreshToken>();

        
        /// <summary>
        /// Вычисляет хэш с использованием алгоритма HMAC-SHA256.
        /// </summary>
        /// <param name="data">Данные, для которых будет вычислен хэш.</param>
        /// <param name="key">Ключ для вычисления HMAC.</param>
        /// <returns>Хэш-значение в виде массива байтов.</returns>
        /// <remarks>
        /// Этот метод используется для вычисления подписи токена.
        /// </remarks>
        public byte[] ComputeHmacSha256(string data, byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            }
        }

        /// <summary>
        /// Кодирует данные в формате Base64Url.
        /// </summary>
        /// <param name="input">Массив байтов, который необходимо закодировать.</param>
        /// <returns>Строка, представляющая данные в формате Base64Url.</returns>
        /// <remarks>
        /// Base64Url используется для кодирования заголовка и полезной нагрузки JWT.
        /// В отличие от стандартного Base64, Base64Url не содержит символы '=' для выравнивания и использует другие символы для кодирования.
        /// </remarks>
        public string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        /// <summary>
        /// Декодирует строку в формате Base64Url обратно в массив байтов.
        /// </summary>
        /// <param name="input">Строка, закодированная в формате Base64Url.</param>
        /// <returns>Массив байтов, полученный после декодирования.</returns>
        /// <remarks>
        /// Этот метод используется для декодирования части токена (например, заголовка и полезной нагрузки) обратно в исходный формат.
        /// </remarks>
        public byte[] Base64UrlDecode(string input)
        {
            var output = input.Replace('-', '+').Replace('_', '/');
            switch (output.Length % 4)
            {
                case 2: output += "=="; break;
                case 3: output += "="; break;
            }
            return Convert.FromBase64String(output);
        }
        

        
    }