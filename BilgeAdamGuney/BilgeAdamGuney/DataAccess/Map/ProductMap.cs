using BilgeAdamGuney.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.DataAccess.Map
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            HasKey(m => m.Id);

            Property(m => m.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            Property(m => m.Description)
                .HasColumnType("nvarchar")
                .HasMaxLength(1500)
                .IsRequired();

            Property(m => m.Price)
                .IsOptional();

            Property(m => m.UnitsInStock)
                .IsOptional();

            Property(m => m.ImagePath)
                .HasColumnType("nvarchar")
                .IsOptional();

            HasOptional(m => m.ProductType)
                .WithMany(m => m.Products)
                .HasForeignKey(m => m.ProductTypeId);
        }
    }
}