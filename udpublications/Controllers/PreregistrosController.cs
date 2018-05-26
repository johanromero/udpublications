using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelo.Models;
using Repositorio;

namespace udpublications.Controllers
{
    public class PreregistrosController : Controller
    {
        private readonly UDPUBLISHContext _context;

        public PreregistrosController(UDPUBLISHContext context)
        {
            _context = context;
        }

        // GET: Preregistros
        public async Task<IActionResult> Index()
        {
            var uDPUBLISHContext = _context.Preregistros.Include(p => p.Cv).Include(p => p.Estpr).Include(p => p.Tipopr);
            return View(await uDPUBLISHContext.ToListAsync());
        }

        // GET: Preregistros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preregistros = await _context.Preregistros
                .Include(p => p.Cv)
                .Include(p => p.Estpr)
                .Include(p => p.Tipopr)
                .SingleOrDefaultAsync(m => m.PreregId == id);
            if (preregistros == null)
            {
                return NotFound();
            }

            return View(preregistros);
        }

        // GET: Preregistros/Create
        public IActionResult Create()
        {
            ViewData["CvId"] = new SelectList(_context.Cv, "CvId", "CvId");
            ViewData["EstprId"] = new SelectList(_context.EstadoPrereg, "EstprId", "EstprNombre");
            ViewData["TipoprId"] = new SelectList(_context.TipoPreregistro, "TipoprId", "TipoprNombre");
            return View();
        }

        // POST: Preregistros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PreregId,PreregIdentificacion,PreregNombres,PreregApellidos,PreregEmail,CvId,PreregTematica,PreregAreaProfesional,PreregFechaCreacion,PreregFechaModificacion,TipoprId,EstprId")] Preregistros preregistros)
        {
            if (ModelState.IsValid)
            {
                _context.Add(preregistros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CvId"] = new SelectList(_context.Cv, "CvId", "CvId", preregistros.CvId);
            ViewData["EstprId"] = new SelectList(_context.EstadoPrereg, "EstprId", "EstprNombre", preregistros.EstprId);
            ViewData["TipoprId"] = new SelectList(_context.TipoPreregistro, "TipoprId", "TipoprNombre", preregistros.TipoprId);
            return View(preregistros);
        }

        // GET: Preregistros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preregistros = await _context.Preregistros.SingleOrDefaultAsync(m => m.PreregId == id);
            if (preregistros == null)
            {
                return NotFound();
            }
            ViewData["CvId"] = new SelectList(_context.Cv, "CvId", "CvId", preregistros.CvId);
            ViewData["EstprId"] = new SelectList(_context.EstadoPrereg, "EstprId", "EstprNombre", preregistros.EstprId);
            ViewData["TipoprId"] = new SelectList(_context.TipoPreregistro, "TipoprId", "TipoprNombre", preregistros.TipoprId);
            return View(preregistros);
        }

        // POST: Preregistros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PreregId,PreregIdentificacion,PreregNombres,PreregApellidos,PreregEmail,CvId,PreregTematica,PreregAreaProfesional,PreregFechaCreacion,PreregFechaModificacion,TipoprId,EstprId")] Preregistros preregistros)
        {
            if (id != preregistros.PreregId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preregistros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreregistrosExists(preregistros.PreregId))
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
            ViewData["CvId"] = new SelectList(_context.Cv, "CvId", "CvId", preregistros.CvId);
            ViewData["EstprId"] = new SelectList(_context.EstadoPrereg, "EstprId", "EstprNombre", preregistros.EstprId);
            ViewData["TipoprId"] = new SelectList(_context.TipoPreregistro, "TipoprId", "TipoprNombre", preregistros.TipoprId);
            return View(preregistros);
        }

        // GET: Preregistros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preregistros = await _context.Preregistros
                .Include(p => p.Cv)
                .Include(p => p.Estpr)
                .Include(p => p.Tipopr)
                .SingleOrDefaultAsync(m => m.PreregId == id);
            if (preregistros == null)
            {
                return NotFound();
            }

            return View(preregistros);
        }

        // POST: Preregistros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var preregistros = await _context.Preregistros.SingleOrDefaultAsync(m => m.PreregId == id);
            _context.Preregistros.Remove(preregistros);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreregistrosExists(int id)
        {
            return _context.Preregistros.Any(e => e.PreregId == id);
        }
    }
}
