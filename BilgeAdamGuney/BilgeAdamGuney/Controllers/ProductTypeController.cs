using BilgeAdamGuney.ActionFilters;
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
    public class ProductTypeController : Controller
    {
        
        public ActionResult Index(int id)
        {
            try
            {
                List<Product> listProduct = null;
                using (InteriorContext db = new InteriorContext())
                {
                    ProductType item = db.ProductType.Where(m => m.Id == id).SingleOrDefault();
                    listProduct = item.Products;
                }
                return View(listProduct);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [AuthorizationControl(RoleId = 2)]
        public ActionResult Create()
        {
            FillCategoryDropDown();
            return View();
        }

        [HttpPost]
        [AuthorizationControl(RoleId = 2)]
        public ActionResult Create(ProductTypeCreateModel model)
        {
            try
            {
                FillCategoryDropDown();
                if (model.Name == null)
                    throw new Exception("Tür ismi boş geçilemez.");

                using (InteriorContext db = new InteriorContext())
                {
                    ProductType productType = new ProductType();
                    productType.Name = model.Name;
                    productType.CategoryId = model.CategoryId;
                    db.Entry(productType).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Tür başarıyla eklendi";
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Kaydetme sırasında bir hata meydana geldi: " + ex;
                return View(model);
            }
        }

        public void FillCategoryDropDown()
        {
            using (InteriorContext db = new InteriorContext())
            {
                SelectList categorySelectList = new SelectList(db.Category.ToList(), "Id", "Name");
                ViewBag.Categories = categorySelectList;
            }
        }
    }
}