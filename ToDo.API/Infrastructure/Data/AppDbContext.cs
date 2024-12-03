using Microsoft.EntityFrameworkCore;
using ToDo.API.Domain.Entities;

namespace ToDo.API.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<TaskCategory> TaskCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskItem>()
                .HasOne(ti => ti.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskCategory>()
                .HasIndex(tc => tc.Name)
                .IsUnique();
        }
    }
}
