using BilgeAdamGuney.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.DataAccess.Map
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            HasKey(m => m.Id);

            Property(m => m.OrderDate)
                .HasColumnType("date")
                .IsOptional();

            Property(m => m.RequiredDate)
               .HasColumnType("date")
               .IsOptional();

            Property(m => m.ShippedDate)
               .HasColumnType("date")
               .IsOptional();

            HasRequired(m => m.Member)
                .WithMany(m => m.Orders)
                .HasForeignKey(m=>m.MemberId);

            HasRequired(m => m.OrderAddress)
                .WithMany(m => m.Orders)
                .HasForeignKey(m => m.OrderAddressId)
                .WillCascadeOnDelete(false);
        }
    }
}