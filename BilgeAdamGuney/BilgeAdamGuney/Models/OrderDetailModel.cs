using BilgeAdamGuney.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.Models
{
    public class OrderDetailModel
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public decimal? Price { get; set; }

        public int? CampaignId { get; set; }
        public virtual Campaign Campaign { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}