using MediatR;
using XAuth.Application.DTOs;

namespace XAuth.Application.Commands;

public record SignInUserCommand(string Email, string Password) : IRequest<AuthenticationResult>;