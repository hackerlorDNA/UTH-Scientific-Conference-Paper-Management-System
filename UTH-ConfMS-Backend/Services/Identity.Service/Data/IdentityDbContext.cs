using Microsoft.EntityFrameworkCore;
using Identity.Service.Entities;

namespace Identity.Service.Data;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Author> Authors { get; set; }
}
