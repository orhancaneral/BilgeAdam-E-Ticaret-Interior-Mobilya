using BilgeAdamGuney.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.Models
{
    public class CartModel
    {
        public List<CartProduct> CartProducts { get; set; }
        public decimal TotalAmount { get; set; }
    }
}