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
    //public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    //{
    //    public void Configure(EntityTypeBuilder<OrderItem> builder)
    //    {
    //        builder.HasKey(oi => oi.Id);

    //        builder.HasOne(oi => oi.Order)
    //               .WithMany(o => o.OrderItems)
    //               .HasForeignKey(oi => oi.OrderId);

    //        builder.HasOne(oi => oi.Product)
    //               .WithMany()
    //               .HasForeignKey(oi => oi.ProductId);

    //        builder.Property(oi => oi.Quantity).IsRequired();
    //        builder.Property(oi => oi.UnitPrice).HasColumnType("decimal(10,2)");
    //    }
    //}
}
