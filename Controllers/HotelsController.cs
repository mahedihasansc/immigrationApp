using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Immigration.Models;

namespace Immigration.Controllers
{
    [Authorize(Roles = "Admin,Immigration Staff")]
    public class HotelsController : Controller
    {
        private HotelEntities db = new HotelEntities();
        public ActionResult Index()
        {
            Getway dc = new Getway();            
            return View(dc.HotelList());
        }
        [HttpPost]
        public ActionResult Index(string regionId, string fromDate, string toDate)
        {
            Getway dc = new Getway();
            DateTime date = DateTime.Now;
            int region;
            string fDate = string.Format("{0:MM/dd/yyyy}", date);
            string tDate = string.Format("{0:MM/dd/yyyy}", date);
            if (fromDate !="" || fromDate != null)
            {
                fDate = fromDate;
            }
            if(toDate=="" || toDate == null)
            {
                tDate = toDate;
            }
            if (regionId == "" || regionId == null)
            {
                region = 0;                
            }
            else
            {
                region = Convert.ToInt32(regionId);                
            }
            return View(dc.SearchHotels(region, fDate, tDate));
        }
        public ActionResult Create()
        {
            using (HotelEntities dc = new HotelEntities())
            {
                DateTime date = DateTime.Now;
                ViewBag.ddd = string.Format("{0:MM/dd/yyyy}", date);
                ViewBag.regionId = new SelectList(dc.tblRegions.OrderBy(r => r.regionName).ToList(), "regionId", "regionName");
                ViewBag.capitalId = new SelectList(dc.CapitalCities.OrderBy(c => c.capitalName).ToList(), "capitalId", "capitalName");
                ViewBag.districtId = new SelectList(dc.Districts.OrderBy(r => r.districtName).ToList(), "districtId", "districtName");
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create(tblHotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.tblHotels.Add(hotel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblHotel hotel = db.tblHotels.Find(id);
            ViewBag.regionId = new SelectList(db.tblRegions.ToList(), "regionId", "regionName");
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }
        [HttpPost]
        public ActionResult Edit(tblHotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tblHotel hotel = db.tblHotels.Find(id);
            db.tblHotels.Remove(hotel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult AllCapital(int id)
        {
            using(HotelEntities db = new HotelEntities())
            {
                return Json(db.CapitalCities.Where(c=> c.regionId ==id).ToList());
            }

        }
        public JsonResult AllDistrict(int id)
        {
            using (HotelEntities db = new HotelEntities())
            {
                var dd = db.Districts.Where(d => d.capitalId == id).ToList();
                return Json(dd);
            }
        }
        public JsonResult AllRegion()
        {
            using(HotelEntities db = new HotelEntities())
            {
                return Json(db.tblRegions.ToList());
            }
        }
    }
}