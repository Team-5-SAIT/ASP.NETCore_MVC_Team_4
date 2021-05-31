using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Author: Dzianis Tsishchenka
/// </summary>

namespace SportsPro.BLL
{
    public class UpdateIncidentManager
    {

        public static List<Incidents> GetAllIncidentsByTechnician(int technicianID) //GetAllIncidentsByTechnician
        {
            var context = new SportsProContext();
            var regIncidents = context.Incidents.Include(i => i.Customer).Include(i => i.Product).Where(o => o.TechnicianId == technicianID).ToList();

            return regIncidents;

        }

        public static Technicians FindIechName(int technicianID) //find tech name
        {
            var db = new SportsProContext();
            var technicians = db.Technicians.SingleOrDefault(i => i.TechnicianId == technicianID);
            return technicians;
        }


        public static void update(Incidents incident) //update incident
        {

            var context = new SportsProContext();
            var originalIncident = context.Incidents.Find(incident.IncidentId);
            originalIncident.Description = incident.Description;
            originalIncident.DateClosed = incident.DateClosed;
            context.SaveChanges();
        }

        public static Incidents Find(int id) //find incident by id
        {
            var context = new SportsProContext();
            var incident = context.Incidents.Find(id);
            return incident;
        }




    }

    

    
}
