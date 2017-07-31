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
    [AuthorizationControl(RoleId = 1)]
    public class AddressController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            FillCityDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult Create(AddressCreateModel model)
        {
            try
            {
                FillCityDropDown();
                Member member = null;
                if (Session["Member"] != null)
                    member = (Member)Session["Member"];
                using (InteriorContext db = new InteriorContext())
                {
                    Address address = new Address();
                    address.Name = model.Name;
                    address.Description = model.Description;
                    address.DistrictId = model.DistrictId;
                    address.MemberId = member.Id;

                    db.Entry(address).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Adresiniz başarıyla kaydedildi";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Adres kaydetme sırasında hata meydana geldi" + ex;
            }

            return View();
        }

        public void FillCityDropDown()
        {
            using (InteriorContext db = new InteriorContext())
            {
                ViewBag.CityList = new SelectList(db.City.ToList(), "Id", "Name");
                ViewBag.DistrictList = new SelectList(db.District.ToList(), "Id", "Name");
            }
        }

        public ActionResult GetDistrictsByCity(int cityId)
        {
            SelectList listDistrict = null;
            using (InteriorContext db = new InteriorContext())
            {
                listDistrict = new SelectList(db.District.Where(m => m.CityId == cityId).ToList(),"Id","Name");
            }

            return Json(listDistrict, JsonRequestBehavior.AllowGet);
        }
    }
}