using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Immigration.ViewModel
{
    public class GuestViewModel
    {
        public int guestId { get; set; }
        [Display(Name = "Guest Name")]
        public string guestName { get; set; }
        [Display(Name = "Nationality")]
        public string guestNationality { get; set; }
        [Display(Name = "Passport Number")]
        public string passportNumber { get; set; }
        [Display(Name = "Address")]
        public string guestAddress { get; set; }
        [Display(Name = "Date In")]
        public string inDate { get; set; }
        [Display(Name = "Date Out")]
        public string outDate { get; set; }
        [Display(Name = "Room Number")]
        public string assignedRoomNumber { get; set; }
        [Display(Name = "Passport Image")]
        public string guestDocuments { get; set; }
        [Display(Name = "Added By")]        
        public string addedBy { get; set; }
        [Display(Name = "Added Date")]
        public string addedDate { get; set; }
    }
}