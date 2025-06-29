using XAuth.Domain.Events;

namespace XAuth.Domain.Entities;

public class User : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string NormalizedEmail { get; private set; }
    public string PasswordHash { get; private set; }
    public bool EmailConfirmed { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public ICollection<UserRole> Roles { get; private set; } = new List<UserRole>();

    // Domain methods and events
    public void ConfirmEmail()
    {
        EmailConfirmed = true;
        AddDomainEvent(new EmailConfirmedEvent(Id, Email));
    }
}