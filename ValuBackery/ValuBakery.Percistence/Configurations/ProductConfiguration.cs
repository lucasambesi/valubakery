using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.Entities;

namespace ValuBakery.Percistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(250);

            builder.Property(p => p.Description)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.HasMany(p => p.ProductRecipeVariants)
                    .WithOne(pm => pm.Product)
                    .HasForeignKey(pm => pm.ProductId);

            builder.HasMany(p => p.ProductMaterials)
                   .WithOne(pm => pm.Product)
                   .HasForeignKey(pm => pm.ProductId);

            builder.Property(pm => pm.ApplyProfitToMaterials)
                .HasColumnType("decimal(10,4)");

            builder.Property(pm => pm.ApplyProfitToRecipes)
                .HasColumnType("decimal(10,4)");

            builder.Property(m => m.IsDeleted).HasDefaultValue(false);
        }
    }
}
