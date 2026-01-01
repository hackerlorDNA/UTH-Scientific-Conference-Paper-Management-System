using Microsoft.EntityFrameworkCore;
using UTHConfMS.Core.Entities;

namespace UTHConfMS.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Deadline> Deadlines { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Paper> Papers { get; set; }
        public DbSet<PaperAuthor> PaperAuthors { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Rebuttal> Rebuttals { get; set; }
        public DbSet<Decision> Decisions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Cấu hình Khóa Chính Phức Hợp cho PaperAuthor
            modelBuilder.Entity<PaperAuthor>()
                .HasKey(pa => new { pa.PaperId, pa.UserId });

            // 2. Cấu hình quan hệ 1-1: Conference - Deadline
            modelBuilder.Entity<Conference>()
                .HasOne(c => c.Deadline)
                .WithOne(d => d.Conference)
                .HasForeignKey<Deadline>(d => d.ConfId);

            // 3. Cấu hình quan hệ 1-1: Assignment - Review
            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Review)
                .WithOne(r => r.Assignment)
                .HasForeignKey<Review>(r => r.AssignmentId);

            // 4. Cấu hình quan hệ 1-1: Paper - Rebuttal
            modelBuilder.Entity<Paper>()
                .HasOne(p => p.Rebuttal)
                .WithOne(r => r.Paper)
                .HasForeignKey<Rebuttal>(r => r.PaperId);

            // 5. Cấu hình quan hệ 1-1: Paper - Decision
            modelBuilder.Entity<Paper>()
                .HasOne(p => p.Decision)
                .WithOne(d => d.Paper)
                .HasForeignKey<Decision>(d => d.PaperId);

            // 6. Cấu hình quan hệ Assignment - User (Reviewer)
            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Reviewer)
                .WithMany(u => u.Assignments)
                .HasForeignKey(a => a.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict); // Tránh xóa User làm mất lịch sử assignment

            // 7. Cấu hình quan hệ Decision - User (Chair)
            modelBuilder.Entity<Decision>()
                .HasOne(d => d.Chair)
                .WithMany(u => u.DecisionsMade)
                .HasForeignKey(d => d.ChairId)
                .OnDelete(DeleteBehavior.SetNull); // Xóa Chair thì decision vẫn còn, set ChairId = null
        }
    }
}