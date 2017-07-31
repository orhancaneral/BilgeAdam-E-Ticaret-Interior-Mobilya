using BilgeAdamGuney.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.DataAccess.Map
{
    public class CartMap : EntityTypeConfiguration<Cart>
    {
        public CartMap()
        {
            
        }
    }
}