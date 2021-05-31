using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SportsPro.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Incidents = new HashSet<Incidents>();
        }

        public int CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string CountryId { get; set; }
        [Phone]
        [Required]
        public string Phone { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name ="Name")]
        public string FullName { get { return FirstName + " " + LastName; } }

        public virtual Countries Country { get; set; }
        public virtual ICollection<Incidents> Incidents { get; set; }
    }
}
