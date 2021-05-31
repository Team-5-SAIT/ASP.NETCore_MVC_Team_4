using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsPro.BLL;
using SportsPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Author: Dzianis Tsishchenka
/// </summary>

namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class UpdateIncidentController : Controller
    {
        private readonly SportsProContext _context;
         
        public UpdateIncidentController(SportsProContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["TechnicianId"] = new SelectList(_context.Technicians, "TechnicianId", "Name");//assign context.Technicians to ViewData
                                                                                                  //in order to disply in dropdown list
            return View();
        }

        public IActionResult Incidents()
        {
            try //technician selected case
            {
                
                var technicianID = Convert.ToInt32(Request.Form["ddl"]); //get selected technicianID from drop down list
                TempData["technicianID"] = technicianID.ToString();//store technicianID in tempdata

                var technicians = UpdateIncidentManager.FindIechName(technicianID); //get tech object        
                TempData["Name"] = technicians.Name.ToString(); //store tech name in tempdata


                var regIncidents = UpdateIncidentManager.GetAllIncidentsByTechnician(technicianID); //GetAllIncidentsByTechnician
                return View(regIncidents);
            }


            catch //technician from session during redirection
            {
                var technicianID = Convert.ToInt32(TempData["techniciansID"]); //retreive technicianID from tempdata
                TempData["technicianID"] = technicianID.ToString();//store technicianID in tempdata

                var technicians = UpdateIncidentManager.FindIechName(technicianID); //get tech object        
                TempData["Name"] = technicians.Name.ToString(); //store tech name in tempdata

                var regIncidents = UpdateIncidentManager.GetAllIncidentsByTechnician(technicianID); //GetAllIncidentsByTechnician
                return View(regIncidents);
            }

        }


        // GET: Incident/Edit

        public ActionResult Edit(int id)
        {
            var technicianID = Convert.ToInt32(TempData["technicianID"]); //retreive technicianID from tempdata
            TempData["technicianID"] = technicianID.ToString();////store technicianID in tempdata to maintain selected technician state
            TempData["techniciansID"] = technicianID.ToString();
            var technicians = UpdateIncidentManager.FindIechName(technicianID); //get tech object        
            TempData["Name"] = technicians.Name.ToString(); //store tech name in tempdata

            var incident = UpdateIncidentManager.Find(id);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FullName", incident.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", incident.ProductId);
            return View(incident);
        }

        // POST: Incident/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Incidents incident)
        {
            var technicianID = Convert.ToInt32(TempData["technicianID"]); //retreive technicianID from tempdata
            TempData["technicianID"] = technicianID.ToString();////store technicianID in tempdata to maintain selected technician state
            TempData["techniciansID"] = technicianID.ToString();
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FullName", incident.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", incident.ProductId);
            try
            {
                
                UpdateIncidentManager.update(incident); 

                return RedirectToAction(nameof(Incidents)); //redirect to incidents page
            }
            catch
            {
                return View(incident);
            }
        }

    }
}
