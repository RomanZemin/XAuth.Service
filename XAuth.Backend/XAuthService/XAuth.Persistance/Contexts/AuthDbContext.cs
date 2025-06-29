using Microsoft.EntityFrameworkCore;
using XAuth.Domain.Entities;

namespace XAuth.Persistance.Contexts;

public class AuthDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
    }
}