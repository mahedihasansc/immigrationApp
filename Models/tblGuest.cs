//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Immigration.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.InteropServices;
    using System.Web;

    public partial class tblGuest
    {
        public int guestId { get; set; }
        [Display(Name = "Guest Name")]
        public string guestName { get; set; }
        [Display(Name = "Country")]
        public Nullable<int> guestNationality { get; set; }
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
        public HttpPostedFileBase ImageFile { get; set; }
        public Nullable<int> hotelId { get; set; }
        [Display(Name = "Added By")]
        public string addedBy { get; set; }
        [Display(Name = "Added Date")]
        public string addedDate { get; set; }
    }
}