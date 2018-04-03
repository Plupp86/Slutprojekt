using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Slutprojekt.Stats
{
    public partial class StatsDBContext : DbContext
    {
        public virtual DbSet<Match> Match { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SlutprojektDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>(entity =>
            {
                entity.ToTable("Match", "Stats");

                entity.Property(e => e.Game).HasMaxLength(50);

                entity.HasOne(d => d.Player1Navigation)
                    .WithMany(p => p.MatchPlayer1Navigation)
                    .HasForeignKey(d => d.Player1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Player1");

                entity.HasOne(d => d.Player2Navigation)
                    .WithMany(p => p.MatchPlayer2Navigation)
                    .HasForeignKey(d => d.Player2)
                    .HasConstraintName("FK_Player2");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "Stats");

                entity.Property(e => e.MathMatches).HasDefaultValueSql("((0))");

                entity.Property(e => e.MathWon).HasDefaultValueSql("((0))");

                entity.Property(e => e.MemoryMatches).HasDefaultValueSql("((0))");

                entity.Property(e => e.MemoryWon).HasDefaultValueSql("((0))");

                entity.Property(e => e.TicMatches).HasDefaultValueSql("((0))");

                entity.Property(e => e.TicWon).HasDefaultValueSql("((0))");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
