using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SportsPro.Models
{
    public partial class Registrations
    {
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Product")]
        public string ProductName { get; set; }
        [Required]
        public int CustomerId { get; set; }
    }
}
