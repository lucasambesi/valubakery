using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.Entities;

namespace ValuBakery.Percistence.Configurations
{
  public class ProductRecipeVariantConfiguration : IEntityTypeConfiguration<ProductRecipeVariant>
    {
        public void Configure(EntityTypeBuilder<ProductRecipeVariant> builder)
        {
            builder.HasKey(pm => pm.Id);

            builder.HasOne(pm => pm.Product)
                   .WithMany(p => p.ProductRecipeVariants)
                   .HasForeignKey(pm => pm.ProductId);

            builder.HasOne(pm => pm.RecipeVariant)
                   .WithMany()
                   .HasForeignKey(pm => pm.RecipeVariantId);

            builder.Property(pm => pm.Quantity).HasColumnType("decimal(10,4)");

            builder.Property(m => m.IsDeleted).HasDefaultValue(false);
        }
    }
}
