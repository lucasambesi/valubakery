using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.Entities;

namespace ValuBakery.Percistence.Configurations
{
    //public class ProductConfiguration : IEntityTypeConfiguration<Product>
    //{
    //    public void Configure(EntityTypeBuilder<Product> builder)
    //    {
    //        builder.HasKey(p => p.Id);
    //        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);

    //        builder.HasOne(p => p.RecipeVariant)
    //               .WithMany()
    //               .HasForeignKey(p => p.RecipeVariantId);

    //        builder.HasMany(p => p.ProductMaterials)
    //               .WithOne(pm => pm.Product)
    //               .HasForeignKey(pm => pm.ProductId);
    //    }
    //}
}
