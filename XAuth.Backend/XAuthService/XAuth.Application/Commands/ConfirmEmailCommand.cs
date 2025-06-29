using CSharpFunctionalExtensions;
using MediatR;

namespace XAuth.Application.Commands;

public record ConfirmEmailCommand(Guid UserId, string Token) : IRequest<Result>;