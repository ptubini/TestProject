using Microsoft.EntityFrameworkCore;
using TestProject.Domain;
using TestProject.Domain.Common;

namespace TestProject.Persistence.DatabaseContext
{
    public class LocationDatabaseContext : DbContext
    {
        public LocationDatabaseContext(DbContextOptions<LocationDatabaseContext> dbContext) : base(dbContext) { }

        public override int SaveChanges()
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
            .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.UpdatedAt = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                }

            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.UpdatedAt = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Location> Locations { get; set; }
    }
}
