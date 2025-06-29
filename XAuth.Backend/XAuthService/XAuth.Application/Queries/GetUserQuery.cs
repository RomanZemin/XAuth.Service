using MediatR;
using XAuth.Application.DTOs;

namespace XAuth.Application.Queries;

public record GetUserQuery(Guid UserId) : IRequest<UserDto>;