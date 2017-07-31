using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.DataAccess.Entities
{
    public class Category
    {
        public Category()
        {
            ProductTypes = new List<ProductType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<ProductType> ProductTypes { get; set; }
    }
}