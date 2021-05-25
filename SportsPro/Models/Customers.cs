using System;
using System.Collections.Generic;

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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CountryId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }

        public virtual Countries Country { get; set; }
        public virtual ICollection<Incidents> Incidents { get; set; }
    }
}
