using BilgeAdamGuney.DataAccess.Context;
using BilgeAdamGuney.DataAccess.Entities;
using BilgeAdamGuney.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BilgeAdamGuney.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            Cart cart = null;
            if (Session["Cart"] != null)
                cart = (Cart)Session["Cart"];
            else
                cart = new Cart();

            CartModel cartModel=new CartModel();
            cartModel.CartProducts = cart.CartProducts;
            cartModel.TotalAmount = cart.TotalPrice;

            return View(cartModel);
        }

        public void AddToCart(int id)
        {
            CartProduct cartProduct = new CartProduct();
            using (InteriorContext db = new InteriorContext())
            {
                Product product = db.Product.FirstOrDefault(m => m.Id == id);
                cartProduct.ProductId = product.Id;
                cartProduct.Product = product;
                cartProduct.Quantity = 1;
                cartProduct.Discount = 0;
                Cart.AddToCart(cartProduct);
                ViewBag.TotalProductCount = Cart.CurrentCart.CartProducts.Count;
                ViewBag.TotalCartAmount = Cart.CurrentCart.TotalPrice;
            }
        }

        public bool DeleteProduct(int productId)
        {
            try
            {
                Cart.DeleteProduct(productId);
                ViewBag.TotalProductCount = Cart.CurrentCart.CartProducts.Count;
                ViewBag.TotalCartAmount = Cart.CurrentCart.TotalPrice;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}