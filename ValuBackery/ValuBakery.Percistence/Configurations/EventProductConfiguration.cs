using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.Entities;

namespace ValuBakery.Percistence.Configurations
{
    //public class EventProductConfiguration : IEntityTypeConfiguration<EventProduct>
    //{
    //    public void Configure(EntityTypeBuilder<EventProduct> builder)
    //    {
    //        builder.HasKey(ep => ep.Id);

    //        builder.HasOne(ep => ep.Event)
    //               .WithMany(e => e.EventProducts)
    //               .HasForeignKey(ep => ep.EventId);

    //        builder.HasOne(ep => ep.Product)
    //               .WithMany()
    //               .HasForeignKey(ep => ep.ProductId);

    //        builder.Property(ep => ep.StockAvailable).IsRequired();
    //    }
    //}
}
