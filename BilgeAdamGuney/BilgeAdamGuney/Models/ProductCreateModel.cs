using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.Models
{
    public class ProductCreateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? UnitsInStock { get; set; }
        public int? ProductTypeId { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}