using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.DataAccess.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? UnitsInStock { get; set; }
        public int? ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}