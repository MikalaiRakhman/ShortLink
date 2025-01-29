using Microsoft.EntityFrameworkCore;
using ShortLink.DAL.Identity;
using ShortLink.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ShortLink.DAL.Data
{
    public class ApplicationDbContext: IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<DoubleUrl> DoubleUrls { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DoubleUrl>()
            .HasOne<User>()
            .WithMany(u => u.DoubleUrls)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<DoubleUrl>()
                .Property(d => d.OriginalUrl)
                .IsRequired()
                .HasMaxLength(2048);

            modelBuilder.Entity<DoubleUrl>()
                .Property(d => d.ShortUrl)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<User>()
                .HasMany(u => u.DoubleUrls)
                .WithOne().HasForeignKey(d => d.UserId)
                .IsRequired(false);

            modelBuilder.Entity<RefreshToken>()
                .HasIndex(rt => rt.Token)
                .IsUnique();

            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany()
                .HasForeignKey(rt => rt.UserId);
        }
    }
}
