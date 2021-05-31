using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SportsPro.Models
{
    public partial class Products
    {
        public Products()
        {
            Incidents = new HashSet<Incidents>();
        }

        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Code")]
        public string ProductCode { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Price")]
        public decimal YearlyPrice { get; set; }
        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public virtual ICollection<Incidents> Incidents { get; set; }
    }
}
