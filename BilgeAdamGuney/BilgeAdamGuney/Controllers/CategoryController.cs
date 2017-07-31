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
    [AuthorizationControl(RoleId = 2)]
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            try
            {
                CategoryListModel model = null;
                List<Category> listCategory = null;
                List<CategoryListModel> listCategoryModel = new List<CategoryListModel>();
                using (InteriorContext db = new InteriorContext())
                {
                   listCategory=  db.Category.ToList();

                    foreach (var item in listCategory)
                    {
                        model = new CategoryListModel();
                        model.Name = item.Name;
                        model.ProductTypes = item.ProductTypes;

                        listCategoryModel.Add(model);
                    }
                }
                
                return View(listCategoryModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryCreateModel model)
        {
            try
            {
                if (model.Name == null)
                    throw new Exception("Kategori ismi boş geçilemez.");

                using (InteriorContext db = new InteriorContext())
                {
                    Category category = new Category();
                    category.Name = model.Name;
                    db.Entry(category).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                    TempData["SuccessMessage"]= "Kategori başarıyla eklendi";
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Kaydetme sırasında bir hata meydana geldi: " + ex;
                return View(model);
            }
        }
    }
}