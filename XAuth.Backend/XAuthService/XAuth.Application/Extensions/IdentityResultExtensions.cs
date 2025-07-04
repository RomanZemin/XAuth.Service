using Microsoft.AspNetCore.Identity;
using XAuth.Application.Commands;
using XAuth.Application.DTOs;
using XAuth.Domain.Entities;

namespace XAuth.Application.Extensions;

    /// <summary>
    /// Статический класс, содержащий расширения для обработки результатов аутентификации и входа в систему.
    /// </summary>
    public static class IdentityResultExtensions
    {
        /// <returns>
        /// Возвращает объект <see cref="AuthenticationResponse"/>, содержащий информацию об успехе операции,
        /// списке ошибок и данных пользователя при необходимости.
        /// </returns>
        /// /// </summary>
        public static AuthenticationResponse ToAuthenticationResponse(
            this IdentityResult identityResult,
            SignInResult? signInResult,
            User? user)
        {
            var response = new AuthenticationResponse
            {
                Succeeded = identityResult.Succeeded,
                Errors = identityResult.Succeeded
                    ? null
                    : identityResult.Errors.ToDictionary(e => e.Code, e => e.Description)
            };


            if (signInResult != null)
            {
                response.Succeeded = signInResult.Succeeded;

                response.Errors = new Dictionary<string, string>();


                if (!signInResult.Succeeded)
                {
                    if (signInResult.IsLockedOut)
                    {
                        response.Errors.Add("LockedOut", "User account is locked out.");
                    }

                    if (signInResult.IsNotAllowed)
                    {
                        response.Errors.Add("NotAllowed", "User is not allowed to sign in.");
                    }

                    if (signInResult.RequiresTwoFactor)
                    {
                        response.Errors.Add("TwoFactorRequired", "Two-factor authentication is required.");
                    }
                    else
                    {
                        response.Errors.Add("InvalidCredentials", "Invalid email or password.");
                    }
                }
                else if (user != null)
                {
                    response.Data = new UserData
                    {
                        UserId = user.UserId.ToString(),
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    };

                    response.Access = new AccessTokenDto
                    {
                        Refresh_Token = user.RefreshToken,
                        Jwt = user.JwtToken,
                        ExpiresAt = user.ExpiresAt
                    };
                }
            }

            return response;
        }
    }