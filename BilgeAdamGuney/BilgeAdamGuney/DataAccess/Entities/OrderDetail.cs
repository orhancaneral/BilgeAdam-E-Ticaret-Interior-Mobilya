using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.DataAccess.Entities
{
    public class OrderDetail
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int? CampaignId { get; set; }
        public virtual Campaign Campaign { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}