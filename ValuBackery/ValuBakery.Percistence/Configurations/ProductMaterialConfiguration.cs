using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.Entities;

namespace ValuBakery.Percistence.Configurations
{
    public class ProductMaterialConfiguration : IEntityTypeConfiguration<ProductMaterial>
    {
        public void Configure(EntityTypeBuilder<ProductMaterial> builder)
        {
            builder.HasKey(pm => pm.Id);

            builder.HasOne(pm => pm.Product)
                   .WithMany(p => p.ProductMaterials)
                   .HasForeignKey(pm => pm.ProductId);

            builder.HasOne(pm => pm.Material)
                   .WithMany()
                   .HasForeignKey(pm => pm.MaterialId);

            builder.Property(pm => pm.Quantity).HasColumnType("decimal(10,4)");

            builder.Property(m => m.IsDeleted).HasDefaultValue(false);
        }
    }
}
