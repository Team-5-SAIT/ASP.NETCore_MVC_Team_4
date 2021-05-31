﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SportsPro.Models
{
    public partial class Countries
    {
        public Countries()
        {
            Customers = new HashSet<Customers>();
        }

        public string CountryId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Customers> Customers { get; set; }
    }
}
