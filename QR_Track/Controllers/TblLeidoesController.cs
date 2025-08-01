using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QR_Track.Models;

namespace QR_Track.Controllers
{
    public class TblLeidoesController : Controller
    {
        private readonly QrTrackDbContext _context;

        public TblLeidoesController(QrTrackDbContext context)
        {
            _context = context;
        }

        // GET: TblLeidoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblLeidos.ToListAsync());
        }

        // GET: TblLeidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblLeido = await _context.TblLeidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblLeido == null)
            {
                return NotFound();
            }

            return View(tblLeido);
        }

        // GET: TblLeidoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblLeidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdQr,DtLeido")] TblLeido tblLeido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblLeido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblLeido);
        }

        // GET: TblLeidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblLeido = await _context.TblLeidos.FindAsync(id);
            if (tblLeido == null)
            {
                return NotFound();
            }
            return View(tblLeido);
        }

        // POST: TblLeidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdQr,DtLeido")] TblLeido tblLeido)
        {
            if (id != tblLeido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblLeido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblLeidoExists(tblLeido.Id))
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
            return View(tblLeido);
        }

        // GET: TblLeidoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblLeido = await _context.TblLeidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblLeido == null)
            {
                return NotFound();
            }

            return View(tblLeido);
        }

        // POST: TblLeidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblLeido = await _context.TblLeidos.FindAsync(id);
            if (tblLeido != null)
            {
                _context.TblLeidos.Remove(tblLeido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblLeidoExists(int id)
        {
            return _context.TblLeidos.Any(e => e.Id == id);
        }
    }
}
