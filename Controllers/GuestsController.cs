using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Immigration.Models;
using Immigration.ViewModel;

namespace Immigration.Controllers
{

    public class GuestsController : Controller
    {
        private HotelEntities db = new HotelEntities();
        [Authorize(Roles = "Hotel Staff")]
        public ActionResult Index()
        {
            Getway dc = new Getway();
            string userName = Convert.ToString(Session["userName"]);
            var guests = dc.GuestsList().Where(g => g.addedBy == userName);
            return View(guests);
        }
        [HttpPost]
        public ActionResult Index(string countryId, string fromDate, string toDate)
        {
            Getway dc = new Getway();
            DateTime date = DateTime.Now;
            int country;
            string fDate = string.Format("{0:MM/dd/yyyy}", date);
            string tDate = string.Format("{0:MM/dd/yyyy}", date);
            if (fromDate != "" || fromDate != null)
            {
                fDate = fromDate;
            }
            if (toDate == "" || toDate == null)
            {
                tDate = toDate;
            }
            if (countryId == "" || countryId == null)
            {
                country = 0;
            }
            else
            {
                country = Convert.ToInt32(countryId);
            }
            return View(dc.SearchGuests(country, fDate, tDate));
        }
        public JsonResult LoadCountry()
        {
            using(HotelEntities db = new HotelEntities())
            {
                return Json(db.Countries.ToList());
            }
        }
        [Authorize(Roles = "Hotel Staff")]
        public ActionResult Create()
        {
            using (HotelEntities dc = new HotelEntities())
            {
                string userName = Convert.ToString(Session["userName"]);
                ViewBag.hotelId = dc.tblUsers.Where(u => u.userName == userName).FirstOrDefault().assignedHotel;
                DateTime date = DateTime.Now;
                ViewBag.ddd = string.Format("{0:MM/dd/yyyy}", date);
                ViewBag.guestNationality = new SelectList(dc.Countries.OrderBy(c => c.CountryName).ToList(), "ID", "CountryName");
                return View();
            }
        }
        public ActionResult Save(tblGuest guest)
        {
            string fileName = Path.GetFileNameWithoutExtension(guest.ImageFile.FileName);
            string extension = Path.GetExtension(guest.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            guest.guestDocuments = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            guest.ImageFile.SaveAs(fileName);
            using (HotelEntities db = new HotelEntities())
            {
                db.tblGuests.Add(guest);
                db.SaveChanges();
            }
            var result = 1;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Hotel Staff")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGuest guest = db.tblGuests.Find(id);
            ViewBag.ID = new SelectList(db.Countries, "ID", "CountryName");
            if (guest == null)
            {
                return HttpNotFound();
            }
            return View(guest);
        }

        [HttpPost]
        public ActionResult Edit(tblGuest guest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guest);
        }
        [Authorize(Roles = "Hotel Staff")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tblGuest guest = db.tblGuests.Find(id);
            db.tblGuests.Remove(guest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}