using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SportsPro.Models
{
    public partial class Incidents
    {
        public int IncidentId { get; set; }
     
        public int CustomerId { get; set; }
      
        public int ProductId { get; set; }
     
        public int? TechnicianId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Date Opened")]
        public DateTime DateOpened { get; set; }
     
        [Display(Name = "Date Closed")]
        public DateTime? DateClosed { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Products Product { get; set; }
        public virtual Technicians Technician { get; set; }
    }
}
