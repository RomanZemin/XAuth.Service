using MediatR;
using XAuth.Application.DTOs;

namespace XAuth.Application.Commands;

public record ResetPasswordCommand(string Email, string Password) : IRequest<AuthenticationResult>;