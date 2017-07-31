using BilgeAdamGuney.ActionFilters;
using BilgeAdamGuney.DataAccess.Context;
using BilgeAdamGuney.DataAccess.Entities;
using BilgeAdamGuney.Models;
using BilgeAdamGuney.StaticUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace BilgeAdamGuney.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(MemberSignUpModel model)
        {
            #region ModelState Validation for javascript out

            if (model.FirstName == null || model.FirstName.Length < 2)
                ModelState.AddModelError("FirstName", "İsim kurallara uymuyor.");
            if (model.LastName == null || model.LastName.Length < 2)
                ModelState.AddModelError("LastName", "Soyisim kurallara uymuyor.");
            if (model.Password == null || model.Password.Length < 8)
                ModelState.AddModelError("Password", "Şifre kurallara uymuyor.");
            if (model.PasswordConfirm == null || model.PasswordConfirm.Length < 8)
                ModelState.AddModelError("PasswordConfirm", "Şifre tekrarı kurallara uymuyor.");
            if (model.Password != model.PasswordConfirm)
                ModelState.AddModelError("PasswordsEquals", "Şifre ve tekrarı uyuşmuyor.");
            if (model.Phone == null || model.Phone.Length < 14)
                ModelState.AddModelError("Phone", "Telefon kurallara uymuyor.");
            if (model.Email == null || model.Email.Length < 5)
                ModelState.AddModelError("EmailLength", "Email uzunluğu uymuyor.");
            if (!IsValidEmail(model.Email))
                ModelState.AddModelError("EmailFormat", "Email formatında giriş yapmadınız.");
            if (!ModelState.IsValid)
                return View(model);

            #endregion

            try
            {
                Member member = null;
                using (InteriorContext db = new InteriorContext())
                {
                    member = new Member();
                    member.FirstName = model.FirstName.ToUpper();
                    member.LastName = model.LastName.ToUpper();
                    member.Email = model.Email.ToLower();
                    member.EmailHash = Sha256.getHashSha256(model.Email);
                    member.Phone = model.Phone;
                    member.PasswordHash = Sha256.getHashSha256(model.Password);
                    member.RoleId = 1;

                    db.Entry(member).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                }
                TempData["SuccessMessage"] = "Aramıza katıldığınız için çok mutluyuz :)";
                Session["Member"] = member;

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ExceptionMessage"] = "Sizi kaydetme sırasında hata ile karşılaştık\nAyrıntılı Bilgi: " + ex.Message;
                return View(model);
            }
        }
        public ActionResult SignIn()
        {
            try
            {
                ActionResult actionResult = View();

                if (Request.Cookies["Member"] != null)
                {
                    Member member = SolutionCookie();

                    if (member == null)
                    {
                        TempData["ResultMessage"] = "Kullanıcı bulunamadı";
                        return View();
                    }

                    Session["Member"] = member;
                    actionResult = RedirectToAction("Index", "Home");
                }
                else if (Session["Member"] != null)
                {
                    Member member = (Member)Session["Member"];
                    actionResult = RedirectToAction("Index", "Home");
                }

                return actionResult;
            }
            catch (Exception ex)
            {
                TempData["ResultMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public ActionResult SignIn(MemberSignInModel model)
        {
            ActionResult actionResult = null;

            #region validation
            if (string.IsNullOrWhiteSpace(model.Email))
                ModelState.AddModelError("Email", "Geçersiz email girdiniz.");
            if (string.IsNullOrWhiteSpace(model.Password) || model.Password.Length < 8 || model.Password.Length > 12)
                ModelState.AddModelError("Password", "Şifre en az 8, en çok 12 karakter olmalıdır.");

            #endregion
            if (!ModelState.IsValid)
            {
                TempData["ResultMessage"] = "Bazı noktalarda anlaşamadık;";
                model.Password = "";
                return View(model);
            }
            else
            {
                try
                {
                    Member member = GetMember(model);
                    if (member == null)
                    {
                        TempData["ResultMessage"] = "Kullanıcı adı veya şifrenizi hatalı girdiniz.";
                        model.Password = "";
                        actionResult = View(model);
                    }
                    else
                    {
                        if (model.RememberMe)
                        {
                            HttpCookie accountCookie = new HttpCookie("Member");

                            string emailPasswordHashed = model.Email;
                            emailPasswordHashed += model.Password;

                            accountCookie.Values.Add("member_hash", emailPasswordHashed);
                            accountCookie.Expires = DateTime.Now.AddDays(15);
                            Response.Cookies.Add(accountCookie);
                        }
                        Session["Member"] = member;

                        actionResult = RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception)
                {
                    TempData["ResultMessage"] = "Kullanıcıyı veritabanında ararken hata meydana geldi";
                    return View();
                }
            }

            return actionResult;
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            try
            {
                bool isMailExist = CheckEmailExist(email);

                if (isMailExist)
                {
                    bool isSend = SendResetPasswordMail(email, Sha256.getHashSha256(email), Request.Url.GetLeftPart(UriPartial.Authority));
                    if (isSend)
                        TempData["ResultMessage"] = "Sıfırlama maili adresinize gönderildi";
                    else
                        TempData["ResultMessage"] = "Mail Gönderilemedi";
                }
                else
                {
                    throw new Exception("Kayıtlı böyle bir email yok.");
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["ResultMessage"] = "Mail adresinizde sıkıntı yaşadık : " + ex.Message;
                return View();
            }
        }
        public ActionResult ResetPassword(string id)
        {
            try
            {
                Member member = GetMemberByEmailHascode(id);
                if (member == null)
                    return RedirectToAction("SignIn");
                TempData["ResultMessage"] = string.Format("Hoşgeldiniz {0} {1}.", member.FirstName.ToUpper(), member.LastName[0]);
                Session["emailHashed"] = id;
            }
            catch (Exception)
            {
                TempData["ResultMessage"] = "Kullanıcı bulunamadı";
            }
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(MemberResetPassword model)
        {
            ActionResult actionResult = null;
            string emailHashed = string.Empty;
            if (Session["emailHashed"] != null)
            {
                try
                {
                    if (model.Password != model.ConfirmPassword)
                    {
                        ModelState.AddModelError("ApprovePassword", "Girilen şifreler uyuşmuyor.");
                        return View();
                    }

                    emailHashed = Session["emailHashed"].ToString();
                    Member member = ChangePassword(emailHashed, model.Password);
                    Session["Member"] = member;

                    actionResult = RedirectToAction("SignIn");
                }
                catch (Exception)
                {
                    TempData["ResultMessage"] = "Şifre değiştirme sırasında hata meydana geldi";
                    actionResult = RedirectToAction("SignIn");
                }
            }
            return actionResult;
        }
        public ActionResult SignOut()
        {
            if (Session["Member"] != null)
                Session["Member"] = null;

            if (Response.Cookies["Member"] != null)
            {
                HttpCookie cookie = new HttpCookie("Member");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index", "Home");
        }

        [AuthorizationControl(RoleId = 1)]
        public ActionResult Update()
        {
            Member member = null;
            if (Session["Member"] != null)
                member = (Member)Session["Member"];

            MemberSignUpModel model = new MemberSignUpModel();
            model.Id = member.Id;
            model.FirstName = member.FirstName;
            model.LastName = member.LastName;
            model.Email = member.Email;
            model.Phone = member.Phone;

            return View(model);
        }

        [HttpPost]
        [AuthorizationControl(RoleId = 1)]
        public ActionResult Update(MemberSignUpModel model)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (InteriorContext db = new InteriorContext())
                {
                    Member member = db.Member.Where(m => m.Id == model.Id).SingleOrDefault();
                    if (member == null)
                        return View();
                    member.FirstName = model.FirstName;
                    member.LastName = model.LastName;
                    member.Email = model.Email;
                    member.EmailHash = Sha256.getHashSha256(model.Email);
                    member.PasswordHash = Sha256.getHashSha256(model.Password);
                    member.Phone = model.Phone;

                    db.Entry(member).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Bilgileriniz başarıyla güncellendi.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Güncelleme sırasında hata meydana geldi" + ex;
            }
            return View();
        }

        private Member GetMemberByEmailHascode(string id)
        {
            try
            {
                using (InteriorContext db = new InteriorContext())
                {
                    return db.Member.Where(m => m.EmailHash == id).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private Member ChangePassword(string emailHashed, string password)
        {
            try
            {
                Member member = null;
                using (InteriorContext db = new InteriorContext())
                {
                    member = db.Member.Where(m => m.EmailHash == emailHashed).SingleOrDefault();
                    if (member == null)
                        throw new Exception("Kullanıcı Bulunamadı");

                    member.PasswordHash = Sha256.getHashSha256(password);
                    db.Entry(member).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return member;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool SendResetPasswordMail(string email, string emailHash, string localAddress)
        {
            try
            {
                SmtpClient sc = new SmtpClient();
                sc.Port = 587;
                sc.Host = "smtp.gmail.com";
                sc.EnableSsl = true;
                sc.Credentials = new NetworkCredential("wicked.avril@gmail.com", "orhancaN1907");

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("wicked.avril@gmail.com", "Pehlivanlar");
                mail.To.Add(email);
                mail.Subject = "Şifre Sıfırlama Maili";
                mail.IsBodyHtml = true;
                mail.Body = string.Format("<a href=\"{0}/Member/ResetPassword/{1}\">Şifre Sıfırlamak için tıklayın..</a>", localAddress, emailHash);

                sc.Send(mail);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool CheckEmailExist(string email)
        {
            try
            {
                using (InteriorContext db = new InteriorContext())
                {
                    return db.Member.Where(m => m.Email == email).Single() == null ? false : true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Member SolutionCookie()
        {
            try
            {
                HttpCookie accountCookie = Request.Cookies["Member"];
                string emailPassword = accountCookie.Values["member_hash"];

                string password = emailPassword.Substring(emailPassword.Length - 64);
                string email = emailPassword.Substring(0, emailPassword.Length - password.Length);
                MemberSignInModel model = new MemberSignInModel();
                model.Email = email;
                model.Password = password;
                Member member = GetMember(model);
                return member;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Member GetMember(MemberSignInModel model)
        {
            try
            {
                Member member = null;
                using (InteriorContext db = new InteriorContext())
                {
                    if (model.Password.Length < 64)
                        model.Password = Sha256.getHashSha256(model.Password);

                    member = db.Member.Where(m => m.Email == model.Email && m.PasswordHash == model.Password).SingleOrDefault();
                }
                return member;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}