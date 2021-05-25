using System;
using System.Collections.Generic;

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
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Products Product { get; set; }
        public virtual Technicians Technician { get; set; }
    }
}
