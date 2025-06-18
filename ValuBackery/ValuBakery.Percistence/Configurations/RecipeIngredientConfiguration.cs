using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.Entities;

namespace ValuBakery.Percistence.Configurations
{
    public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            builder.HasKey(ri => ri.Id);

            builder.HasOne(ri => ri.RecipeVariant)
                   .WithMany(r => r.Ingredients)
                   .HasForeignKey(ri => ri.RecipeVariantId);

            builder.HasOne(ri => ri.Ingredient)
                   .WithMany()
                   .HasForeignKey(ri => ri.IngredientId);

            builder.Property(ri => ri.Quantity).HasColumnType("decimal(10,2)");
        }
    }
}
