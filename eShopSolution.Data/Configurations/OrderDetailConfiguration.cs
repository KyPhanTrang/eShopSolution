using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(od => new { od.OrderId, od.ProductId });

            builder.HasOne(o => o.Order)
                .WithMany(od => od.OrderDetails)
                .HasForeignKey(od=>od.OrderId);

            builder.HasOne(p => p.Product)
                .WithMany(od=>od.OrderDetails)
                .HasForeignKey(od  => od.ProductId);
        }
    }
}
