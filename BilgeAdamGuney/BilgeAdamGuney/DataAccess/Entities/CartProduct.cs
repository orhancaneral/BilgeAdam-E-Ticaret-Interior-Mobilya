using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.DataAccess.Entities
{
    public class CartProduct
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public decimal TotalAmount { get { return (Convert.ToDecimal(Product.Price)*Quantity); } }

        public virtual List<Cart> Carts { get; set; }
    }
}