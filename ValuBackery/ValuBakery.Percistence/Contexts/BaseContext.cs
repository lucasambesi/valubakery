using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ValuBakery.Data.Entities;
using ValuBakery.Data.Helpers;
using ValuBakery.Data.Entities.Common;

namespace ValuBakery.Percistence.Contexts
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateHelper.CurrenctUtcNow();
                        entry.Entity.UpdatedAt = DateHelper.CurrenctUtcNow();
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateHelper.CurrenctUtcNow();
                        break;

                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
