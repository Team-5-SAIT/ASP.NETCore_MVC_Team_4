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

namespace SportsPro.Controllers
{
    [Authorize]
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


        // GET: Incident/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidents = await _context.Incidents.FindAsync(id);

            if (incidents == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FullName", incidents.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", incidents.ProductId);
            ViewData["TechnicianId"] = new SelectList(_context.Technicians, "TechnicianId", "Email", incidents.TechnicianId);

            var technicianID = Convert.ToInt32(TempData["technicianID"]); //retreive technicianID from tempdata
            TempData["technicianID"] = technicianID.ToString();////store technicianID in tempdata to maintain selected technician state
            TempData["techniciansID"] = technicianID.ToString();
            return View(incidents);
        }

        // POST: Incident/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IncidentId,CustomerId,ProductId,Title,DateOpened")] Incidents incidents)
        {
            
            var technicianID = Convert.ToInt32(TempData["technicianID"]); //retreive technicianID from tempdata
            TempData["technicianID"] = technicianID.ToString();////store technicianID in tempdata to maintain selected technician state
            TempData["techniciansID"] = technicianID.ToString();
            if (id != incidents.IncidentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incidents);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncidentsExists(incidents.IncidentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
                return RedirectToAction(nameof(Incidents));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FullName", incidents.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", incidents.ProductId);
            ViewData["TechnicianId"] = new SelectList(_context.Technicians, "TechnicianId", "Email", incidents.TechnicianId);

            
            return View(incidents);
        }

        private bool IncidentsExists(int id)
        {
            return _context.Incidents.Any(e => e.IncidentId == id);
        }
    }
}
