using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.DataAccess.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}