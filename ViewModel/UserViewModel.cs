using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Immigration.ViewModel
{
    public class UserViewModel
    {
        public int userId { get; set; }
        [Display(Name = "Person Name")]
        public string personName { get; set; }
        [Display(Name = "User Name")]
        public string userName { get; set; }
        [Display(Name = "User Type")]
        public string typeName { get; set; }
        [Display(Name = "Hotel Name")]
        public string hotelName { get; set; }
        [Display(Name = "Status")]
        public string statusName { get; set; }
        [Display(Name = "Added By")]
        public string addedBy { get; set; }
        [Display(Name = "Added Date")]
        public string addedDate { get; set; }
    }
}