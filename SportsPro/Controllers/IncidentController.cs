using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IncidentController : Controller
    {
        private readonly SportsProContext _context;

        public IncidentController(SportsProContext context)
        {
            _context = context;
        }

        // GET: Incident
        public async Task<IActionResult> Index()
        {
            var sportsProContext = _context.Incidents.Include(i => i.Customer).Include(i => i.Product).Include(i => i.Technician);
            return View(await sportsProContext.ToListAsync());
        }

        // GET: Incident/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidents = await _context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .Include(i => i.Technician)
                .FirstOrDefaultAsync(m => m.IncidentId == id);
            if (incidents == null)
            {
                return NotFound();
            }

            return View(incidents);
        }

        // GET: Incident/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            ViewData["TechnicianId"] = new SelectList(_context.Technicians, "TechnicianId", "Email");
            return View();
        }

        // POST: Incident/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IncidentId,CustomerId,ProductId,TechnicianId,Title,Description,DateOpened,DateClosed")] Incidents incidents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incidents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", incidents.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", incidents.ProductId);
            ViewData["TechnicianId"] = new SelectList(_context.Technicians, "TechnicianId", "Email", incidents.TechnicianId);
            return View(incidents);
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", incidents.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", incidents.ProductId);
            ViewData["TechnicianId"] = new SelectList(_context.Technicians, "TechnicianId", "Email", incidents.TechnicianId);
            return View(incidents);
        }

        // POST: Incident/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IncidentId,CustomerId,ProductId,TechnicianId,Title,Description,DateOpened,DateClosed")] Incidents incidents)
        {
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", incidents.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", incidents.ProductId);
            ViewData["TechnicianId"] = new SelectList(_context.Technicians, "TechnicianId", "Email", incidents.TechnicianId);
            return View(incidents);
        }

        // GET: Incident/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidents = await _context.Incidents
                .Include(i => i.Customer)
                .Include(i => i.Product)
                .Include(i => i.Technician)
                .FirstOrDefaultAsync(m => m.IncidentId == id);
            if (incidents == null)
            {
                return NotFound();
            }

            return View(incidents);
        }

        // POST: Incident/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incidents = await _context.Incidents.FindAsync(id);
            _context.Incidents.Remove(incidents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncidentsExists(int id)
        {
            return _context.Incidents.Any(e => e.IncidentId == id);
        }
    }
}
