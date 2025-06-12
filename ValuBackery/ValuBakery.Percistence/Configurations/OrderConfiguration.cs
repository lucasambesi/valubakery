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
    //public class OrderConfiguration : IEntityTypeConfiguration<Order>
    //{
    //    public void Configure(EntityTypeBuilder<Order> builder)
    //    {
    //        builder.HasKey(o => o.Id);
    //        builder.Property(o => o.Date).IsRequired();

    //        builder.HasOne(o => o.Customer)
    //               .WithMany()
    //               .HasForeignKey(o => o.CustomerId);

    //        builder.HasMany(o => o.OrderItems)
    //               .WithOne(oi => oi.Order)
    //               .HasForeignKey(oi => oi.OrderId);
    //    }
    //}
}
