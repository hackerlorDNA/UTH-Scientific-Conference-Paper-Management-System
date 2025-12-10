using Microsoft.EntityFrameworkCore;
using UTHConfMS.Core.Entities;

namespace UTHConfMS.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Paper> Papers => Set<Paper>();
        public DbSet<Conference> Conferences => Set<Conference>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Conflict> Conflicts => Set<Conflict>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    }
}
