using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.Entities;

namespace ValuBakery.Percistence.Configurations
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).IsRequired().HasMaxLength(150);
            builder.Property(m => m.Description).HasMaxLength(150);
            builder.Property(m => m.CostPerUnit).HasPrecision(18, 2);
            builder.Property(m => m.Unit).IsRequired();
            builder.Property(m => m.IsDeleted).HasDefaultValue(false);
        }
    }
}
