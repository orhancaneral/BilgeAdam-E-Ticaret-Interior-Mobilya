using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.DataAccess.Entities
{
    public class Campaign
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}