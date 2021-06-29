using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace VemVinner.Data
{
    public partial class BaseDbContext : DbContext
    {
        public BaseDbContext()
        {
        }

        public BaseDbContext(DbContextOptions<BaseDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Achievement> Achievements { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupGame> GroupGames { get; set; }
        public virtual DbSet<GroupGameEvent> GroupGameEvents { get; set; }
        public virtual DbSet<GroupGameEventUserResult> GroupGameEventUserResults { get; set; }
        public virtual DbSet<GroupUser> GroupUsers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAchievement> UserAchievements { get; set; }
        public virtual DbSet<UserStatistic> UserStatistics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Achievement>(entity =>
            {
                entity.ToTable("Achievement");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.InsertByUser).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsertDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Game");

                entity.HasIndex(e => e.Name, "IdxGame_Name")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.InsertByUser).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsertDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ProfilePictureUrl)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("ProfilePictureURL");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.InsertByUser).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsertDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<GroupGame>(entity =>
            {
                entity.ToTable("GroupGame");

                entity.Property(e => e.InsertByUser).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsertDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GroupGames)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GroupGame_GameFK");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupGames)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GroupGame_GroupFK");
            });

            modelBuilder.Entity<GroupGameEvent>(entity =>
            {
                entity.ToTable("GroupGameEvent");

                entity.Property(e => e.EventTime)
                    .HasPrecision(0)
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.InsertByUser).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsertDate).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GroupGameEvents)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GroupGameEvent_GameFK");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupGameEvents)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GroupGameEvent_GroupFK");
            });

            modelBuilder.Entity<GroupGameEventUserResult>(entity =>
            {
                entity.ToTable("GroupGameEventUserResult");

                entity.Property(e => e.InsertByUser).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsertDate).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.GroupGameEvent)
                    .WithMany(p => p.GroupGameEventUserResults)
                    .HasForeignKey(d => d.GroupGameEventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GroupGameEventUserResult_GroupGameEventFK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupGameEventUserResults)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GroupGameEventUserResult_UserFK");
            });

            modelBuilder.Entity<GroupUser>(entity =>
            {
                entity.ToTable("GroupUser");

                entity.Property(e => e.InsertByUser).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsertDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupUsers)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GroupUser_GroupFK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GroupUser_UserFK");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Username, "IdxUser_Username")
                    .IsUnique();

                entity.Property(e => e.InsertByUser).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsertDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<UserAchievement>(entity =>
            {
                entity.ToTable("UserAchievement");

                entity.Property(e => e.InsertByUser).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsertDate).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Achievement)
                    .WithMany(p => p.UserAchievements)
                    .HasForeignKey(d => d.AchievementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserAchievement_AchievementFK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAchievements)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserAchievement_UserFK");
            });

            modelBuilder.Entity<UserStatistic>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("UserStatistics_PK");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.InsertByUser).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsertDate).HasDefaultValueSql("(getutcdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
