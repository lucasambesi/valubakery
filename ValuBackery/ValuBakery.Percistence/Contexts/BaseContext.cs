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
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<RecipeVariant> RecipeVariant { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeComponent> RecipeComponents { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<Material> Materials { get; set; }
        //public DbSet<ProductMaterial> ProductMaterials { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderItem> OrderItems { get; set; }
        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<Expense> Expenses { get; set; }
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
