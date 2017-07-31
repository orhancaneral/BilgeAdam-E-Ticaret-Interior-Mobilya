using BilgeAdamGuney.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
        public int OrderAddressId { get; set; }
        public virtual Address OrderAddress { get; set; }
        public virtual District District { get; set; }
        public virtual City City { get; set; }
        public virtual List<Product> Products { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}