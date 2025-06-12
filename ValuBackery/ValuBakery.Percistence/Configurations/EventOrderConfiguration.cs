using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.Entities;

namespace ValuBakery.Percistence.Configurations
{
    //public class EventOrderConfiguration : IEntityTypeConfiguration<EventOrder>
    //{
    //    public void Configure(EntityTypeBuilder<EventOrder> builder)
    //    {
    //        builder.HasKey(eo => eo.Id);

    //        builder.HasOne(eo => eo.Event)
    //               .WithMany(e => e.EventOrders)
    //               .HasForeignKey(eo => eo.EventId);

    //        builder.HasOne(eo => eo.Order)
    //               .WithMany()
    //               .HasForeignKey(eo => eo.OrderId);
    //    }
    //}
}
