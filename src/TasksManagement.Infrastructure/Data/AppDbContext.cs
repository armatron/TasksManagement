using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TasksManagement.Domain.Entities;

namespace TasksManagement.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TasksEntity> Tasks { get; set; }

        /* doesn't work with InMemory
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TasksEntity>()
                .Property(e => e.CreatedDate)
                .ValueGeneratedOnAdd() // Only set during insertion
                .Metadata.IsReadOnlyAfterSave = true; // Prevent updates after saving
        }
        */

        /* implemented in repository 
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // set CreatedDate and UpdatedDate before saving
            var entries = ChangeTracker.Entries<TasksEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    //entry.Entity.Id = Guid.NewGuid();
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.UpdatedDate = null;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedDate").IsModified = false;   // do not change value on update
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        */
    }
}
