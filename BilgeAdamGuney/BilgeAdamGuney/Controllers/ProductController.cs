using BilgeAdamGuney.ActionFilters;
using BilgeAdamGuney.DataAccess.Context;
using BilgeAdamGuney.DataAccess.Entities;
using BilgeAdamGuney.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BilgeAdamGuney.Controllers
{
    public class ProductController : Controller
    {
        [AuthorizationControl(RoleId = 2)]
        public ActionResult Index()
        {
            try
            {
                ProductListModel model = null;
                List<Product> listProduct = null;
                List<ProductListModel> listProductModel = new List<ProductListModel>();
                using (InteriorContext db = new InteriorContext())
                {
                    listProduct = db.Product.ToList();

                    foreach (var item in listProduct)
                    {
                        model = new ProductListModel();
                        model.Name = item.Name.ToUpper();
                        model.Price = item.Price;
                        model.UnitsInStock = item.UnitsInStock;
                        model.ProductType = item.ProductType;
                        model.ImagePath = item.ImagePath;

                        listProductModel.Add(model);
                    }
                }

                return View(listProductModel);
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
        public ActionResult Create(ProductCreateModel model)
        {
            try
            {
                FillCategoryDropDown();

                if (model.Name == null||model.Name.Length<2)
                    ModelState.AddModelError("Name", "Ürün ismi çok kısa veya boş");

                if (!ModelState.IsValid)
                    return View();

                using (InteriorContext db = new InteriorContext())
                {
                    Product product = new Product();
                    product.Name = model.Name;
                    product.Description = model.Description;
                    product.Price = model.Price;
                    product.UnitsInStock = model.UnitsInStock;
                    product.ProductTypeId = model.ProductTypeId;
                    if (model.ImageFile != null)
                        product.ImagePath = SavePhoto(model.ImageFile);
                    else
                        product.ImagePath = "/assets/img/owner/defaultImage.jpg";

                    db.Entry(product).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Ürün başarıyla eklendi";
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ürün ekleme sırasında hata meydana geldi: " + ex;
                return View(model);
            }
        }
        
        public ActionResult Single(int id)
        {
            ProductListModel product = null;
            try
            {
                using (InteriorContext db = new InteriorContext())
                {
                    product = db.Product.Include("ProductType.Products").Where(m => m.Id == id)
                        .Select(m => new ProductListModel()
                        {
                            Id = m.Id,
                            Name = m.Name,
                            Description = m.Description,
                            Price = m.Price,
                            ImagePath = m.ImagePath,
                            ProductTypeId = m.ProductTypeId,
                            ProductType = m.ProductType,
                            UnitsInStock = m.UnitsInStock
                        }).SingleOrDefault();
                TempData["SimilarProducts"] = product.ProductType.Products.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            
            return View(product);
        }

        public void FillCategoryDropDown()
        {
            using (InteriorContext db = new InteriorContext())
            {
                SelectList productSelectList = new SelectList(db.ProductType.ToList(), "Id", "Name");
                ViewBag.ProductTypes = productSelectList;
            }
        }

        
        public string SavePhoto(HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                    file.SaveAs(Server.MapPath("~/assets/img/saved/" + file.FileName));
                
                return "/assets/img/saved/" + file.FileName;
            }
            catch (Exception ex)
            {
                throw new Exception("Resim kaydetme sırasında hata meydana geldi" + ex);
            }
        }
    }
}