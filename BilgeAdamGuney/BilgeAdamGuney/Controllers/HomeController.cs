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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                using (InteriorContext db = new InteriorContext())
                {
                    //HttpApplicationState Orhancan = HttpContext.ApplicationInstance.Application;
                    TempData["Categories"] = db.Category.Include("ProductTypes").ToList();
                    ViewBag.LastProductsFirst = db.Product.OrderByDescending(m=>m.Id).Take(4).ToList();
                    ViewBag.LastProductsSecond = db.Product.OrderByDescending(m => m.Id).Skip(4).Take(4).ToList();
                    ViewBag.TotalProductCount = Cart.CurrentCart.CartProducts.Count;
                    ViewBag.TotalCartAmount = Cart.CurrentCart.TotalPrice;
                    ViewBag.BestSellers = db.OrderDetail.GroupBy(m => m.ProductId)
                                            .Select(mp=>
                                                new
                                                {
                                                    ProductId = mp.Key,
                                                    CountSell=mp.Sum(w=>w.Quantity)
                                                }    
                                            ).OrderByDescending(m=>m.CountSell);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }

        public ActionResult IndexAdmin()
        {
            Member member = null;
            if (Session["Member"] != null)
                member = (Member)Session["Member"];
            MemberSignUpModel model = new MemberSignUpModel();
            model.FirstName = member.FirstName;
            model.LastName = member.LastName;

            return View(model);
        }
    }
}