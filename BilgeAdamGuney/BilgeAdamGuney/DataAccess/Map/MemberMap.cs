using BilgeAdamGuney.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.DataAccess.Map
{
    public class MemberMap:EntityTypeConfiguration<Member>
    {
        public MemberMap()
        {
            HasKey(m => m.Id);

            Property(m => m.FirstName)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            Property(m => m.LastName)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            Property(m => m.RoleId)
                .IsRequired();

            Property(m => m.DateOfBirth)
                .HasColumnType("date")
                .IsOptional();

            Property(m => m.Email)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            Property(m => m.EmailHash)
                .HasColumnType("nvarchar")
                .HasMaxLength(64)
                .IsOptional();

            Property(m => m.PasswordHash)
                .HasColumnType("nvarchar")
                .HasMaxLength(64)
                .IsRequired();

            Property(m => m.Phone)
                .HasColumnType("nvarchar")
                .HasMaxLength(20)
                .IsRequired();

            Property(m => m.Gender)
                .IsOptional();
        }
    }
}