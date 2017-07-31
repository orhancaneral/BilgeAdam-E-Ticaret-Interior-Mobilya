using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgeAdamGuney.DataAccess.Entities
{
    public class Cart
    {
        public static Cart CurrentCart
        {
            get
            {
                HttpContext context = HttpContext.Current;
                if (context.Session["Cart"] == null)
                    context.Session["Cart"] = new Cart();

                return (Cart)context.Session["Cart"];
            }
        }

        private List<CartProduct> _cartProducts = new List<CartProduct>();
        public virtual List<CartProduct> CartProducts
        {
            get { return _cartProducts; }
            set { _cartProducts = value; }
        }

        public static void AddToCart(CartProduct cartProduct)
        {
            if (HttpContext.Current.Session["Cart"] != null)
            {
                Cart cart = (Cart)HttpContext.Current.Session["Cart"];
                if (cart.CartProducts.Any(m => m.ProductId == cartProduct.ProductId))
                    cart.CartProducts.FirstOrDefault(m => m.ProductId == cartProduct.ProductId).Quantity++;
                else
                    cart.CartProducts.Add(cartProduct);

            }
            else
            {
                Cart cart = new Cart();
                cart.CartProducts.Add(cartProduct);
                HttpContext.Current.Session["Cart"] = cart;
            }
        }
        public static void Clear()
        {
            HttpContext.Current.Session["Cart"] = new Cart();
        }

        public decimal TotalPrice { get { return CartProducts.Sum(m => m.TotalAmount); } }

        public static bool DeleteProduct(int id)
        {
            Cart cart = CurrentCart;
            return CurrentCart.CartProducts.Remove(cart.CartProducts.Where(m => m.ProductId == id).SingleOrDefault());
        }
    }
}