using BilgeAdamGuney.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.DataAccess.Map
{
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            HasKey(m => m.Id);

            Property(m => m.Description)
                .HasColumnType("nvarchar")
                .HasMaxLength(500)
                .IsRequired();

            Property(m => m.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            HasRequired(m => m.District)
                .WithMany(m => m.Addresses)
                .HasForeignKey(m=>m.DistrictId);

            HasRequired(m => m.Member)
                .WithMany(m => m.Addresses)
                .HasForeignKey(m => m.MemberId);
        }
    }
}