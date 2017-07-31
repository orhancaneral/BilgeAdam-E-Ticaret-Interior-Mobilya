using BilgeAdamGuney.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.DataAccess.Map
{
    public class CityMap : EntityTypeConfiguration<City>
    {
        public CityMap()
        {
            HasKey(m => m.Id);

            Property(m => m.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(30)
                .IsRequired();

        }
    }
}