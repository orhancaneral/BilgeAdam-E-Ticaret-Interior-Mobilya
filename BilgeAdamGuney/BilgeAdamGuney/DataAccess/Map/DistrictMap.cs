using BilgeAdamGuney.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.DataAccess.Map
{
    public class DistrictMap : EntityTypeConfiguration<District>
    {
        public DistrictMap()
        {
            HasKey(m => m.Id);

            Property(m => m.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            HasRequired(m => m.City)
                .WithMany(m => m.Districts)
                .HasForeignKey(m => m.CityId);

        }
    }
}