using XAuth.Domain.Entities.Base;
using XAuth.Domain.Events;

namespace XAuth.Domain.Entities;

public class User : AggregateRoot
{
    public Guid UserId { get; private set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    public string? Email { get; private set; }
    public string? NormalizedEmail { get; private set; }
    public string? PasswordHash { get; private set; }
    public bool EmailConfirmed { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public DateTime? UpdatedAt { get; private set; }
    
    public ICollection<UserRole> Roles { get; private set; } = new List<UserRole>();
    
    public string? Jwt { get; set; }
    
    public string? JwtToken { get; set; }
    public string? RefreshToken { get; set; }
    
    public string? ExpiresAt { get; set; }

    public void ConfirmEmail()
    {
        EmailConfirmed = true;
        AddDomainEvent(new EmailConfirmedEvent(UserId, Email));
    }
}