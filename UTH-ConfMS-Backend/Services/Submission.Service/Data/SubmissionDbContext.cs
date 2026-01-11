using Microsoft.EntityFrameworkCore;
using Submission.Service.Entities;

namespace Submission.Service.Data;

public class SubmissionDbContext : DbContext
{
    public SubmissionDbContext(DbContextOptions<SubmissionDbContext> options)
        : base(options) { }

    public DbSet<Paper> Papers { get; set; }
    public DbSet<PaperAuthor> PaperAuthors { get; set; }
    public DbSet<Rebuttal> Rebuttals { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<SubmissionFile> SubmissionFiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PaperAuthor>()
            .HasKey(pa => new { pa.PaperId, pa.UserId });

        modelBuilder.Entity<Paper>()
            .HasOne(p => p.Rebuttal)
            .WithOne(r => r.Paper)
            .HasForeignKey<Rebuttal>(r => r.PaperId);
    }
}
