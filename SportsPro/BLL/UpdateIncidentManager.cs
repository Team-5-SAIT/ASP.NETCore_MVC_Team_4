using SportsPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SportsPro.BLL
{
    public class UpdateIncidentManager
    {

        public static List<Incidents> GetAllIncidentsByTechnician(int technicianID) //GetAllIncidentsByTechnician
        {
            var context = new SportsProContext();
            var regIncidents = context.Incidents.Where(o => o.TechnicianId == technicianID).ToList();
            return regIncidents;
        }

        public static Technicians FindIechName(int technicianID) //find tech name
        {
            var db = new SportsProContext();
            var technicians = db.Technicians.SingleOrDefault(i => i.TechnicianId == technicianID);
            return technicians;
        }
    }

    

    
}
