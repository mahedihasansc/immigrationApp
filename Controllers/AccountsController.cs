using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Immigration.Models;
using System.Data.Entity;
using System.Web.UI;

namespace Immigration.Controllers
{    
    public class AccountsController : Controller
    {
        [Authorize(Roles = "Admin,Immigration Staff")]
        public ActionResult Index()
        {
            Getway db = new Getway();
            ViewBag.hotelUser = db.HotelUsers();
            ViewBag.immigrationUser = db.ImmigrationUsers();
            return View();
        }
        [Authorize(Roles = "Admin,Immigration Staff")]
        public ActionResult Register()
        {
            using (HotelEntities dc = new HotelEntities())
            {
                ViewBag.hotelId = new SelectList(dc.tblHotels.ToList(), "hotelId", "hotelName");
                ViewBag.typeId = new SelectList(dc.tblUserTypes.ToList(), "typeId", "typeName");
                DateTime date = DateTime.Now;
                ViewBag.ddd = string.Format("{0:MM/dd/yyyy}", date);
                return View();
            }
        }
        [HttpPost]
        public ActionResult Register(tblUser user)
        {
            using (HotelEntities dc = new HotelEntities())
            {
                if (ModelState.IsValid)
                {
                    if (user.userType != 3)
                    {
                        user.assignedHotel = 0;
                    }
                    string name = FormsAuthentication.HashPasswordForStoringInConfigFile(user.userName, "SHA1");
                    user.userHash = name;
                    string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(user.password, "SHA1");
                    user.passwordHash = pass;
                    dc.tblUsers.Add(user);
                    dc.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(tblUser user, string ReturnUrl = "")
        {
            using (HotelEntities dc = new HotelEntities())
            {
                string name = FormsAuthentication.HashPasswordForStoringInConfigFile(user.userName, "SHA1");
                user.userHash = name;
                string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(user.password, "SHA1");
                user.passwordHash = pass;
                var count = dc.tblUsers.Where(u => u.userHash == user.userHash && u.passwordHash == user.passwordHash && u.activeStatus==1).Count();
                if (count > 0)
                {
                    Session["userName"] = user.userName;
                    FormsAuthentication.SetAuthCookie(user.userName, false);
                    if (ReturnUrl == null || ReturnUrl == "")
                    {
                        var uType = dc.tblUsers.Where(a => a.userName == user.userName).FirstOrDefault().userType;
                        if (uType != 3)
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Guests");
                        }
                    }
                    else
                    {
                        return Redirect(ReturnUrl);
                    }
                }
                ViewBag.error = "user name or password not match.";
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["userName"] = "";
            return RedirectToAction("Login");
        }
        public ActionResult ChangeStatus(int id)
        {
            using (HotelEntities dc = new HotelEntities())
            {
                var user = dc.tblUsers.Find(id);
                int status = Convert.ToInt32(user.activeStatus);
                if (status == 1)
                {
                    user.activeStatus = 2;
                }
                else
                {
                    user.activeStatus = 1;
                }
                if (user != null)
                {
                    dc.Entry(user).State = EntityState.Modified;
                    dc.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
        public JsonResult CheckUserName(string userName)
        {
            using (HotelEntities db = new HotelEntities())
            {
                System.Threading.Thread.Sleep(100);
                int value = db.tblUsers.Where(u => u.userName == userName).Count();
                if (value != 0)
                {
                    return Json(value);
                }
                else
                {
                    return Json(0);
                }
            }
        }
        [Authorize]
        public ActionResult PasswordReset()
        {
            return View();
        }        
        public JsonResult ResetPassword(string userName, string oldPass, string newPass)
        {
            var dd = 0;
            using (HotelEntities db = new HotelEntities())
            {
                var user = db.tblUsers.Where(u => u.userName == userName && u.password == oldPass).FirstOrDefault();
                if (user != null)
                {
                    user.password = newPass;
                    string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(newPass, "SHA1");
                    user.passwordHash = pass;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    dd = 1;
                    return Json(dd);
                }
                else
                {
                    return Json(dd);
                }
            }
        }
        public JsonResult CheckUserType(string userName)
        {
            using (HotelEntities db = new HotelEntities())
            {
                var uType = db.tblUsers.Where(a => a.userName == userName).FirstOrDefault().userType;
                return Json(uType);
            }
        }
    }
}