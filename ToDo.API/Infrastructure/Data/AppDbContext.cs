using Microsoft.EntityFrameworkCore;
using ToDo.API.Domain.Entities;

namespace ToDo.API.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MissionItem> TaskItems { get; set; }
        public DbSet<MissionCategory> TaskCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MissionItem>()
                .HasOne(mi => mi.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MissionCategory>()
                .HasIndex(mc => mc.Name)
                .IsUnique();
        }
    }
}
