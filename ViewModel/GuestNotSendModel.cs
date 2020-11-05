using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Immigration.ViewModel
{
    public class GuestNotSendModel
    {
        public int No { get; set; }
        [Display(Name = "Hotel Name")]
        public string hotelName { get; set; }
        [Display(Name = "Region")]
        public string regionName { get; set; }
    }
}