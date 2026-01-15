using Microsoft.EntityFrameworkCore;
using Review.Service.Entities;

namespace Review.Service.Data;

public class ReviewDbContext : DbContext
{
    public ReviewDbContext(DbContextOptions<ReviewDbContext> options)
        : base(options) { }

    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<PaperReview> Reviews { get; set; }
    public DbSet<Decision> Decisions { get; set; }
    public DbSet<Conflict> Conflicts { get; set; }
    public DbSet<Reviewer> Reviewers { get; set; }
    public DbSet<ReviewerInvitation> ReviewerInvitations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.PaperReview)
            .WithOne(r => r.Assignment)
            .HasForeignKey<PaperReview>(r => r.AssignmentId);

        modelBuilder.Entity<Reviewer>()
            .HasIndex(r => new { r.UserId, r.ConferenceId })
            .IsUnique();
    }
}
