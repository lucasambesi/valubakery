using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.Entities;

namespace ValuBakery.Percistence.Configurations
{
    public class RecipeComponentConfiguration : IEntityTypeConfiguration<RecipeComponent>
    {
        public void Configure(EntityTypeBuilder<RecipeComponent> builder)
        {
            builder.HasKey(rc => rc.Id);

            builder.HasOne(rc => rc.ParentRecipeVariant)
                   .WithMany(r => r.Components)
                   .HasForeignKey(rc => rc.ParentRecipeVariantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rc => rc.ChildRecipeVariant)
                   .WithMany(r => r.UsedIn)
                   .HasForeignKey(rc => rc.ChildRecipeVariantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(rc => rc.Quantity).HasColumnType("decimal(10,4)");
        }
    }
}
