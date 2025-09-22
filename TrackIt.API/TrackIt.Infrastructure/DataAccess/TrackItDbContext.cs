using Microsoft.EntityFrameworkCore;
using TrackIt.Domain.Entities;

namespace TrackIt.Infrastructure.DataAccess
{
    public class TrackItDbContext : DbContext
    {
        public TrackItDbContext(DbContextOptions<TrackItDbContext> options) : base(options)
        {
        }

        public DbSet<TodoItemEntity> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoItemEntity>()
                        .HasIndex(t => t.Name)
                        .HasDatabaseName("IX_TodoItems_Name");
        }
    }
}