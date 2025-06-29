using MediatR;
using XAuth.Application.Commands;
using XAuth.Application.DTOs;
using XAuth.Application.Interfaces;
using XAuth.Domain.Entities;
using XAuth.Domain.Events;

namespace XAuth.Application.Handlers;

public class RegisterUserCommandHandler : IRequestHandler<SignUpUserCommand, AuthenticationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEventBus _eventBus;

    public async Task<AuthenticationResult> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Email, _passwordHasher.Hash(request.Password));
        await _userRepository.AddAsync(user);

        await _eventBus.Publish(new UserRegisteredEvent(user.Id, user.Email, user.NormalizedEmail), cancellationToken);

        return new AuthenticationResult(user.Id, GenerateJwtToken(user));
    }

    private string GenerateJwtToken(User user)
    {
        throw new NotImplementedException();
    }
}