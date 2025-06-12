using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.Entities;

namespace ValuBakery.Percistence.Configurations
{
    //public class EventConfiguration : IEntityTypeConfiguration<Event>
    //{
    //    public void Configure(EntityTypeBuilder<Event> builder)
    //    {
    //        builder.HasKey(e => e.Id);
    //        builder.Property(e => e.Name).IsRequired().HasMaxLength(250);
    //        builder.Property(e => e.Date).IsRequired();

    //        builder.HasMany(e => e.EventProducts)
    //               .WithOne(ep => ep.Event)
    //               .HasForeignKey(ep => ep.EventId);

    //        builder.HasMany(e => e.EventOrders)
    //               .WithOne(ep => ep.Event)
    //               .HasForeignKey(ep => ep.EventId);
    //    }
    //}
}
