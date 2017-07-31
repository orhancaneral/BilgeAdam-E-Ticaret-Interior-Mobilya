using BilgeAdamGuney.ActionFilters;
using BilgeAdamGuney.DataAccess.Context;
using BilgeAdamGuney.DataAccess.Entities;
using BilgeAdamGuney.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BilgeAdamGuney.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizationControl(RoleId =1)]
        public ActionResult GetByMember(int id)
        {
            List<OrderModel> listOrder = null;
            try
            {
                using (InteriorContext db = new InteriorContext())
                {
                    listOrder = db.Order.Where(m => m.MemberId == id)
                        .Select(m => new OrderModel()
                        {
                            MemberId = m.MemberId,
                            OrderAddress = m.OrderAddress,
                            OrderAddressId = m.OrderAddressId,
                            District = m.OrderAddress.District,
                            City = m.OrderAddress.District.City,
                            OrderDate = m.OrderDate,
                            RequiredDate = m.RequiredDate,
                            ShippedDate = m.ShippedDate,
                            OrderDetails = m.OrderDetails,
                            Id = m.Id
                        }
                    ).ToList();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Siparişlerin gösterimi sırasında hata meydana geldi" + ex;
            }

            return View(listOrder);
        }

        [AuthorizationControl(RoleId = 1)]
        public ActionResult GetOrderProducts(int id)
        {
            List<OrderDetailModel> listOrderDetail = null;
            using (InteriorContext db = new InteriorContext())
            {
                listOrderDetail = db.OrderDetail.Where(m => m.OrderId == id)
                    .Select(m=>new OrderDetailModel()
                    {
                        ProductId=m.ProductId,
                        Product=m.Product,
                        ImagePath=m.Product.ImagePath,
                        Price=m.Product.Price,
                        Name=m.Product.Name
                    }
                    ).ToList();
            }

            return View(listOrderDetail);
        }
        [AuthorizationControl(RoleId = 1)]
        public ActionResult Create()
        {
            if (Session["Member"] == null)
                return RedirectToAction("SignIn", "Member");

            return View(FillAddressDropdown());
        }

        [HttpPost]
        [AuthorizationControl(RoleId = 1)]
        public ActionResult Create(OrderModel model)
        {
            DbContextTransaction transaction;
            using (InteriorContext db = new InteriorContext())
            {
                transaction = db.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                try
                {
                    Cart cart = null;
                    if (Session["Cart"] != null)
                        cart = Cart.CurrentCart;

                    Member member = null;
                    if (Session["Member"] != null)
                        member = (Member)Session["Member"];
                    else
                        return RedirectToAction("SignIn", "Member");

                    if (cart.CartProducts.Count == 0)
                    {
                        TempData["ErrorMessage"] = "Sepetinizde Ürün Yok";
                        return RedirectToAction("Index", "Cart");
                    }

                    Order order = new Order();
                    order.MemberId = member.Id;
                    order.OrderAddressId = model.OrderAddressId;
                    order.OrderDate = DateTime.Now;
                    db.Entry(order).State = EntityState.Added;
                    db.SaveChanges();

                    OrderDetail orderDetail = null;
                    foreach (var item in cart.CartProducts)
                    {
                        orderDetail = new OrderDetail();
                        orderDetail.ProductId = item.ProductId;
                        orderDetail.Quantity = item.Quantity;
                        orderDetail.UnitPrice = item.TotalAmount;
                        orderDetail.OrderId = order.Id;
                        db.Entry(orderDetail).State = EntityState.Added;
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    Cart.Clear();
                    TempData["SuccessMessage"] = "Siparişiniz Onaylandı!";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Sipariş alınırken hata meydana geldi" + ex;
                    transaction.Rollback();
                }
                finally
                {
                    transaction.Dispose();
                }
            }
            return View(FillAddressDropdown());
        }


        public void FillCityAndDistrictDropDown()
        {
            using (InteriorContext db = new InteriorContext())
            {
                ViewBag.CityList = new SelectList(db.City.ToList(), "Id", "Name");
                ViewBag.DistrictList = new SelectList(db.District.ToList(), "Id", "Name");
            }
        }

        public OrderModel FillAddressDropdown()
        {
            Member member = null;
            if (Session["Member"] != null)
            {
                member = (Member)Session["Member"];
                using (InteriorContext db = new InteriorContext())
                {
                    Member memberFromDb = db.Member.Where(m => m.Id == member.Id).SingleOrDefault();
                    SelectList selectListAddress = new SelectList(memberFromDb.Addresses, "Id", "Name");
                    ViewBag.AddressList = selectListAddress;
                }
            }
            else
                member = new Member();

            OrderModel orderModel = new OrderModel();
            orderModel.Member = member;


            return orderModel;
        }
    }
}