using BilgeAdamGuney.DataAccess.Entities;
using BilgeAdamGuney.DataAccess.Map;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.DataAccess.Context
{
    public class InteriorContext : DbContext
    {
        public InteriorContext()
            :base("InteriorDatabase")
        {

        }

        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Campaign> Campaign { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>().HasKey(q =>
                new {
                    q.ProductId,
                    q.OrderId
                });

            modelBuilder.Entity<OrderDetail>()
                .HasRequired(t => t.Order)
                .WithMany(t => t.OrderDetails)
                .HasForeignKey(t => t.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasRequired(t => t.Product)
                .WithMany(t => t.OrderDetails)
                .HasForeignKey(t => t.ProductId);

            modelBuilder.Configurations.Add(new MemberMap());
            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new DistrictMap());
            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new CampaignMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductTypeMap());
            
        }
    }
}