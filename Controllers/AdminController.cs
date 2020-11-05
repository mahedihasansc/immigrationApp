using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Immigration.Models;

namespace Immigration.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "Admin,Immigration Staff")]
        public ActionResult Index()
        {
            using (HotelEntities dc = new HotelEntities())
            {
                DateTime date = DateTime.Now;
                string d = string.Format("{0:MM/dd/yyyy}", date);

                ViewBag.totalUsers = dc.tblUsers.Count();
                ViewBag.totalHotels = dc.tblHotels.Count();
                ViewBag.totalGuests = dc.tblGuests.Count();

                ViewBag.todaysGuests = dc.tblGuests.Where(g => g.addedDate == d).Count();
                ViewBag.todaysHotels = dc.tblHotels.Where(g => g.addedDate == d).Count();
                ViewBag.todaysUsers = dc.tblUsers.Where(g => g.addedDate == d).Count();

                Getway db = new Getway();
                ViewBag.notSend = db.NotSendGuest(d);
            }
            return View();
        }
        [Authorize(Roles = "Admin,Immigration Staff")]
        public ActionResult Guest()
        {
            Getway dc = new Getway();
            return View(dc.GuestsList());
        }
        public JsonResult LoadCapitals()
        {
            using (HotelEntities db = new HotelEntities())
            {
                return Json(db.CapitalCities.ToList());
            }
        }
        public JsonResult LoadDistricts(int id)
        {
            Getway db = new Getway();
            return Json(db.LoadDistricts(id));
        }
        public JsonResult LoadHotels(int id)
        {
            Getway db = new Getway();
            return Json(db.LoadHotelsByDistricts(id));
        }
        [HttpPost]
        public ActionResult Guest(string capital, string district, string hotel, string fromDate, string toDate)
        {
            Getway dc = new Getway();
            DateTime date = DateTime.Now;
            int cap, dis, hot;
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
            if (capital == "" || capital == null)
            {
                cap = 0;
            }
            else
            {
                cap = Convert.ToInt32(capital);
            }
            if (district == "" || district == null)
            {
                dis = 0;
            }
            else
            {
                dis = Convert.ToInt32(district);
            }
            if (hotel == "" || hotel == null)
            {
                hot = 0;
            }
            else
            {
                hot = Convert.ToInt32(hotel);
            }
            return View(dc.SearchGuestsByCapitalDistrictAndHotel(cap,dis,hot, fDate, tDate));
        }
        [Authorize(Roles = "Admin,Immigration Staff")]
        public ActionResult Capitals()
        {
            using (HotelEntities dc = new HotelEntities())
            {
                ViewBag.regionId = new SelectList(dc.tblRegions.OrderBy(r => r.regionName).ToList(), "regionId", "regionName");
                return View();
            }               
        }
        [HttpPost]
        public ActionResult Capitals(CapitalCity capital)
        {
            if (ModelState.IsValid) 
            {
                using (HotelEntities db = new HotelEntities())
                {
                    db.CapitalCities.Add(capital);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public JsonResult CheckCapitalName(string capitalName)
        {
            using (HotelEntities db = new HotelEntities())
            {
                System.Threading.Thread.Sleep(100);
                int value = db.CapitalCities.Where(u => u.capitalName == capitalName).Count();
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
        [Authorize(Roles = "Admin,Immigration Staff")]
        public ActionResult Districts()
        {
            using (HotelEntities dc = new HotelEntities())
            {
                ViewBag.capitalId = new SelectList(dc.CapitalCities.OrderBy(r => r.capitalName).ToList(), "capitalId", "capitalName");
                return View();
            }
        }
        [HttpPost]
        public ActionResult Districts(District district)
        {
            if (ModelState.IsValid)
            {
                using (HotelEntities db = new HotelEntities())
                {
                    db.Districts.Add(district);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public JsonResult CheckDistrictName(string districtName)
        {
            using (HotelEntities db = new HotelEntities())
            {
                System.Threading.Thread.Sleep(100);
                int value = db.Districts.Where(u => u.districtName == districtName).Count();
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
    }
}