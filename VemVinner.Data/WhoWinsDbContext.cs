using Microsoft.EntityFrameworkCore;

using VemVinner.Data.ComplexModel;

#nullable disable

namespace VemVinner.Data
{
    public partial class WhoWinsDbContext : BaseDbContext
    {
        public WhoWinsDbContext()
        {
        }

        public WhoWinsDbContext(DbContextOptions<BaseDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<sp_searchUsers_Result> SP_SearchUsers_Result { get; set; }
        public virtual DbSet<sp_searchGames_Result> SP_SearchGames_Result { get; set; }
        public virtual DbSet<sp_getGroups_Result> SP_GetGroups_Result { get; set; }
        public virtual DbSet<sp_getGroupUsers_Result> SP_GetGroupUsers_Result { get; set; }
        public virtual DbSet<sp_getGroupGames_Result> SP_GetGroupGames_Result { get; set; }
        public virtual DbSet<sp_getGroupGameUserPlacements_Result> SP_GetGroupGameUserPlacements_Result { get; set; }
        public virtual DbSet<sp_getLatestGroupGameEvents_Result> SP_GetLatestGroupGameEvents_Result { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<sp_searchUsers_Result>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<sp_searchGames_Result>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<sp_getGroups_Result>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<sp_getGroupUsers_Result>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<sp_getGroupGames_Result>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<sp_getGroupGameUserPlacements_Result>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<sp_getLatestGroupGameEvents_Result>(entity =>
            {
                entity.HasNoKey();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
