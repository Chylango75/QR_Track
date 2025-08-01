using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QR_Track.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QR_Track.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PersonaController : Controller
    {
        private readonly QrTrackDbContext _context;

        public PersonaController(QrTrackDbContext context)
        {
            _context = context;
        }

        // GET: Persona
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblPersonas.ToListAsync());
        }

        // GET: Persona/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPersona = await _context.TblPersonas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblPersona == null)
            {
                return NotFound();
            }

            return View(tblPersona);
        }

        // GET: Persona/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Persona/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] TblPersona tblPersona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblPersona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblPersona);
        }

        // GET: Persona/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPersona = await _context.TblPersonas.FindAsync(id);
            if (tblPersona == null)
            {
                return NotFound();
            }
            return View(tblPersona);
        }

        // POST: Persona/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] TblPersona tblPersona)
        {
            if (id != tblPersona.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblPersona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblPersonaExists(tblPersona.Id))
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
            return View(tblPersona);
        }

        // GET: Persona/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPersona = await _context.TblPersonas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblPersona == null)
            {
                return NotFound();
            }

            return View(tblPersona);
        }

        // POST: Persona/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblPersona = await _context.TblPersonas.FindAsync(id);
            if (tblPersona != null)
            {
                _context.TblPersonas.Remove(tblPersona);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblPersonaExists(int id)
        {
            return _context.TblPersonas.Any(e => e.Id == id);
        }
    }
}
