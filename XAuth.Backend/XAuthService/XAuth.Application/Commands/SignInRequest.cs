using MediatR;
using XAuth.Application.DTOs;

namespace XAuth.Application.Commands;

public record SignInRequest(string Email, string Password, bool RememberMe) : IRequest<AuthenticationResult>;
