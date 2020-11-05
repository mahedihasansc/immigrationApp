using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Immigration.ViewModel
{
    using System.ComponentModel.DataAnnotations;
    public class HotelViewModel
    {
        public int hotelId { get; set; }
        [Display(Name = "Hotel Name")]
        public string hotelName { get; set; }
        [Display(Name = "Region")]
        public string hotelRegion { get; set; }
        [Display(Name = "Capital")]
        public string capitalCity { get; set; }
        [Display(Name = "District")]
        public string districts { get; set; }
        [Display(Name = "Address")]
        public string hotelAddress { get; set; }
        [Display(Name = "Contact Person")]
        public string contactPersonName { get; set; }
        [Display(Name = "Contact No")]
        public string contactNumber { get; set; }
        [Display(Name = "Added By")]
        public string addedBy { get; set; }
        [Display(Name = "Added Date")]
        public string addedDate { get; set; }
    }
    public class LoadHotel
    {
        public int hotelId { get; set; }
        public string hotelName { get; set; }
    }
}