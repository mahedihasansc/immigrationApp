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

    public partial class CapitalCity
    {
        public int capitalId { get; set; }
        [Display(Name ="Capital Name")]
        public string capitalName { get; set; }
        [Display(Name ="Region Name")]
        public Nullable<int> regionId { get; set; }
    }
}
