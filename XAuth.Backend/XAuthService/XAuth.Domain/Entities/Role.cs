using XAuth.Domain.Entities.Base;

namespace XAuth.Domain.Entities;

public class Role : Entity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string NormalizedName { get; private set; }
    public ICollection<UserRole> Users { get; private set; } = new List<UserRole>();
}