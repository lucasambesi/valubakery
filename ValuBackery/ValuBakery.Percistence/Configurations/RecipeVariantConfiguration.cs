using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.Entities;
using Microsoft.VisualBasic;

namespace ValuBakery.Percistence.Configurations
{
    public class RecipeVariantConfiguration : IEntityTypeConfiguration<RecipeVariant>
    {
        public void Configure(EntityTypeBuilder<RecipeVariant> builder)
        {
            builder.ToTable("RecipeVariant");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).IsRequired().HasMaxLength(200);
            builder.Property(r => r.Portions).IsRequired().HasMaxLength(100);
        }
    }
}