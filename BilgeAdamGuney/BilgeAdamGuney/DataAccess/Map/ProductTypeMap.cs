using BilgeAdamGuney.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.DataAccess.Map
{
    public class ProductTypeMap : EntityTypeConfiguration<ProductType>
    {
        public ProductTypeMap()
        {
            HasKey(m => m.Id);

            Property(m => m.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            HasRequired(m => m.Category)
                .WithMany(m => m.ProductTypes)
                .HasForeignKey(m => m.CategoryId);
        }
    }
}