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
    
    public partial class tblUser
    {
        public int userId { get; set; }
        [Display(Name = "Person Name")]
        public string personName { get; set; }
        [Display(Name = "User Name")]
        public string userName { get; set; }
        public string userHash { get; set; }
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string passwordHash { get; set; }
        [Display(Name = "User Type")]
        public Nullable<int> userType { get; set; }
        [Display(Name = "Assigned Hotel")]
        public Nullable<int> assignedHotel { get; set; }
        [Display(Name = "Status")]
        public Nullable<int> activeStatus { get; set; }
        [Display(Name = "Added By")]
        public string addedBy { get; set; }
        [Display(Name = "Added Date")]
        public string addedDate { get; set; }
    }
}