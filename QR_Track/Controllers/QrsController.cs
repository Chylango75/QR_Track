using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QR_Track.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace QR_Track.Controllers
{
    public class QrsController : Controller
    {
        private readonly QrTrackDbContext _context;

        public QrsController(QrTrackDbContext context)
        {
            _context = context;
        }

        // GET: Qrs
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 20;
            var list = _context.TblQrs.ToList().ToPagedList(pageNumber, pageSize);
            return View(list);
        }

        public JsonResult QRsGetN(string term)
        {
            var lst = _context.TblQrs.ToList()
                //.Where(p => string.IsNullOrEmpty(texto) || p.Nombre.Contains(texto))
                .Where(p => p.Descripcion.ToLower().Contains(term.ToLower()))
                .ToList();
            return Json(lst);
        }

        // GET: Qrs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblQr = await _context.TblQrs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblQr == null)
            {
                return NotFound();
            }

            return View(tblQr);
        }

        // GET: Qrs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Qrs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,IdPersona")] TblQr tblQr)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblQr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblQr);
        }

        // GET: Qrs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblQr = await _context.TblQrs.FindAsync(id);
            if (tblQr == null)
            {
                return NotFound();
            }
            return View(tblQr);
        }

        // POST: Qrs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,IdPersona")] TblQr tblQr)
        {
            if (id != tblQr.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblQr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblQrExists(tblQr.Id))
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
            return View(tblQr);
        }

        // GET: Qrs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblQr = await _context.TblQrs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblQr == null)
            {
                return NotFound();
            }

            return View(tblQr);
        }

        // POST: Qrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblQr = await _context.TblQrs.FindAsync(id);
            if (tblQr != null)
            {
                _context.TblQrs.Remove(tblQr);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblQrExists(int id)
        {
            return _context.TblQrs.Any(e => e.Id == id);
        }
    }
}
