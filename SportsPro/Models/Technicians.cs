﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SportsPro.Models
{
    public partial class Technicians
    {
        public Technicians()
        {
            Incidents = new HashSet<Incidents>();
        }

        public int TechnicianId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Incidents> Incidents { get; set; }
    }
}
