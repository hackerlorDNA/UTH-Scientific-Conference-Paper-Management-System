using Microsoft.EntityFrameworkCore;
using Conference.Service.Entities;

namespace Conference.Service.Data;

public class ConferenceDbContext : DbContext
{
    public ConferenceDbContext(DbContextOptions<ConferenceDbContext> options)
        : base(options) { }

    public DbSet<Entities.Conference> Conferences { get; set; }
    public DbSet<Deadline> Deadlines { get; set; }
    public DbSet<Track> Tracks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entities.Conference>()
            .HasOne(c => c.Deadline)
            .WithOne(d => d.Conference)
            .HasForeignKey<Deadline>(d => d.ConfId);
    }
}
