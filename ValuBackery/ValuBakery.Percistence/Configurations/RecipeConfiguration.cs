using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuBakery.Data.Entities;

namespace ValuBakery.Percistence.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("Recipes");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Description)
                .HasMaxLength(250);

            builder.Property(r => r.IsDeleted)
                .IsRequired();

            builder.HasMany(r => r.Variants)
                .WithOne(v => v.Recipe)
                .HasForeignKey(v => v.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
