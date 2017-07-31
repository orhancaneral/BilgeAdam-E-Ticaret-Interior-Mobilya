using BilgeAdamGuney.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.DataAccess.Map
{
    public class CampaignMap : EntityTypeConfiguration<Campaign>
    {
        public CampaignMap()
        {
            HasKey(m => m.Id);

            Property(m => m.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

        }
    }
}