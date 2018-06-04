using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            var uDPUBLISHContext = _context.Preregistros.Include(p => p.Estpr).Include(p => p.Tipopr);
            return View(await uDPUBLISHContext.ToListAsync());
        }

        // GET: Preregistros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preregistro = await _context.Preregistros
                .Include(p => p.Estpr)
                .Include(p => p.Tipopr)
                .Include(p => p.Evaluacion)
                .ThenInclude(eval => eval.Usr)
                .SingleOrDefaultAsync(m => m.PreregId == id);
            if (preregistro == null)
            {
                return NotFound();
            }
            var evaluacion = new Evaluacion();
            evaluacion.PreregId = preregistro.PreregId;

            return View(new PreregistrosViewModel { preregistros = preregistro,
                                                    evaluacion = evaluacion });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Evaluar([FromForm]  PreregistrosViewModel vm)
        {

            if (ModelState.IsValid)
            {
                vm.evaluacion.UsrId = 1;
                //vm.evaluacion.PreregId = vm.preregistros.PreregId;

                _context.Add(vm.evaluacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           return View(vm);
        }

            // GET: Preregistros/Create
            public IActionResult Create([FromQuery(Name = "TipoPreRegistro")] string tipoPreregistro)
        {

           
            ViewData["EstprId"] = new SelectList(_context.EstadoPrereg, "EstprId", "EstprNombre");
            var qTipoP = _context.TipoPreregistro;
            var tiposPRERList = new SelectList(qTipoP, "TipoprId", "TipoprNombre");
            ViewData["TipoprId"] = tiposPRERList;
            var _Tipopr = qTipoP.Where(x => x.TipoprId == Int32.Parse(tipoPreregistro)).First();
            var preRegistro = new Preregistros
            {
                PreregFechaCreacion = DateTime.Now,
                PreregFechaModificacion = DateTime.Now,
                Tipopr = _Tipopr,
                TipoprId = _Tipopr.TipoprId,
                EstprId = 1
            };


            return View(preRegistro);
        }

        // POST: Preregistros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]  Preregistros pregistro, IFormFile formFile)
        {
            if (formFile == null && pregistro.TipoprId == 2)
            {
                ModelState.AddModelError("formFile", "Por favor adjunte el CV(Hoja de Vida)");
                ViewData["EstprId"] = new SelectList(_context.EstadoPrereg, "EstprId", "EstprNombre", pregistro.EstprId);
                ViewData["TipoprId"] = new SelectList(_context.TipoPreregistro, "TipoprId", "TipoprNombre", pregistro.TipoprId);

                return View(pregistro);
            }

            if (pregistro.TipoprId == 2)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);

                    pregistro.PreregAdjunto = memoryStream.ToArray();
             
                }


            }

            pregistro.PreregFechaCreacion = DateTime.Now;
            pregistro.PreregFechaModificacion = DateTime.Now;
            pregistro.EstprId = 1;

            //if (ModelState.IsValid)
            //{
                _context.Add(pregistro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //} else {
            //   var errors =  ModelState.Values.Where(x => x.Errors.Count >= 1);

            //}
            //ViewData["CvId"] = new SelectList(_context.Cv, "CvId", "CvId", viewModel.PReg.CvId);
            //ViewData["EstprId"] = new SelectList(_context.EstadoPrereg, "EstprId", "EstprNombre", pregistro.EstprId);
            //ViewData["TipoprId"] = new SelectList(_context.TipoPreregistro, "TipoprId", "TipoprNombre", pregistro.TipoprId);

            //return View(pregistro);
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
 

       
        public async Task <IActionResult> DownloadCV(int id)
        {
            var preregistro = await _context.Preregistros.SingleOrDefaultAsync(m => m.PreregId == id);

            if (preregistro.PreregAdjunto == null)
            {
                Response.StatusCode = 404;
                return View("AdjuntoNotFound");
            }

            string fileName = "CV_" + preregistro.PreregIdentificacion;
            return File(preregistro.PreregAdjunto, "application/pdf");

        }
    }
}
