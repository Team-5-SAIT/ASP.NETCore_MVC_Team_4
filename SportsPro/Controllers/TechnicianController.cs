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
    public class TechnicianController : Controller
    {
        private readonly SportsProContext _context;

        public TechnicianController(SportsProContext context)
        {
            _context = context;
        }

        // GET: Technician
        public async Task<IActionResult> Index()
        {
            return View(await _context.Technicians.ToListAsync());
        }

        // GET: Technician/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technicians = await _context.Technicians
                .FirstOrDefaultAsync(m => m.TechnicianId == id);
            if (technicians == null)
            {
                return NotFound();
            }

            return View(technicians);
        }

        // GET: Technician/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Technician/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TechnicianId,Name,Email,Phone")] Technicians technicians)
        {
            if (ModelState.IsValid)
            {
                _context.Add(technicians);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(technicians);
        }

        // GET: Technician/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technicians = await _context.Technicians.FindAsync(id);
            if (technicians == null)
            {
                return NotFound();
            }
            return View(technicians);
        }

        // POST: Technician/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TechnicianId,Name,Email,Phone")] Technicians technicians)
        {
            if (id != technicians.TechnicianId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(technicians);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechniciansExists(technicians.TechnicianId))
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
            return View(technicians);
        }

        // GET: Technician/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technicians = await _context.Technicians
                .FirstOrDefaultAsync(m => m.TechnicianId == id);
            if (technicians == null)
            {
                return NotFound();
            }

            return View(technicians);
        }

        // POST: Technician/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var technicians = await _context.Technicians.FindAsync(id);
            _context.Technicians.Remove(technicians);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TechniciansExists(int id)
        {
            return _context.Technicians.Any(e => e.TechnicianId == id);
        }
    }
}
