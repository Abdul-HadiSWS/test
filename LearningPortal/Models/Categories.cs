﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LearningPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Categories
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Enter Category Name.")]
        [Display(Name = "Catecory Name")]
        [StringLength(55, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Index("INDEX_Title", IsUnique = true)]
        public string CategoryName { get; set; }

       
        public virtual ICollection<SubCategories> SubCategories { get; set; }
    }
}
