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

            builder.HasOne(rc => rc.ParentRecipe)
                   .WithMany(r => r.Components)
                   .HasForeignKey(rc => rc.ParentRecipeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rc => rc.ChildRecipe)
                   .WithMany(r => r.UsedIn)
                   .HasForeignKey(rc => rc.ChildRecipeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(rc => rc.Quantity).HasColumnType("decimal(10,2)");
        }
    }
}
