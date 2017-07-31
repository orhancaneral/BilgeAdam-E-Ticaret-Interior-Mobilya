using BilgeAdamGuney.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.Models
{
    public class CategoryListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ProductType> ProductTypes { get; set; }
    }
}