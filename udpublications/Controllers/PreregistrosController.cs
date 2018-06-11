using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelo.Models;
using Repositorio;
using Servicios;

namespace udpublications.Controllers
{
    public class PreregistrosController : Controller
    {
        private readonly UDPUBLISHContext _context;

        public PreregistrosController(UDPUBLISHContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "ADMINISTRADOR,EVALUADOR")]
        // GET: Preregistros
        public async Task<IActionResult> Index()
        {
            return View(await PreregistroService.ObtenerListadoPreregistros(_context));
        }

        // GET: Preregistros/Details/5
        [Authorize(Roles = "EVALUADOR,COMITÉ")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var preregistro = await _context.Preregistros
            //    .Include(p => p.Estpr)
            //    .Include(p => p.Tipopr)
            //    .Include(p => p.Evaluacion)
            //    .ThenInclude(eval => eval.Usr)
            //    .SingleOrDefaultAsync(m => m.PreregId == id);
            var preregistro = await PreregistroService.ObtenerPreregistroXId(_context, id.Value);
            if (preregistro == null)
            {
                return NotFound();
            }

            var evaluacion = new Evaluacion();
            evaluacion.PreregId = preregistro.PreregId;
            LLenarDiccionarioUIXRol(preregistro);

            return View(new PreregistrosViewModel { preregistros = preregistro,
                evaluacion = evaluacion });
        }

        public void LLenarDiccionarioUIXRol(Preregistros preregistro )
        {
            IQueryable<EstadoPrereg> estadosXRol;

            if (User.IsInRole("EVALUADOR"))
            {
                estadosXRol = _context.EstadoPrereg.Where(p => (p.EstprId == 2 || p.EstprId == 3 || p.EstprId == 4));
            }
            else
            {
                estadosXRol = _context.EstadoPrereg.Where(p => (p.EstprId == 5));
            }
            ViewData["EstprId"] = new SelectList(estadosXRol, "EstprId", "EstprNombre", preregistro.EstprId);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMINISTRADOR,EVALUADOR")]
        public async Task<IActionResult> Evaluar([FromForm]  PreregistrosViewModel vm)
        {

            if (ModelState.IsValid)
            {
                vm.evaluacion.UsrId = 1;
                vm.evaluacion.EvalFecha = DateTime.Now;

                var preregInicial = await _context.Preregistros
                            .Include(p => p.Evaluacion)
                            .ThenInclude(eval => eval.Usr)
                            .SingleOrDefaultAsync(m => m.PreregId == vm.evaluacion.PreregId);

                preregInicial.EstprId = vm.EstprId;

                _context.Add(vm.evaluacion);
                _context.Update(preregInicial);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           return View(vm);
        }

            // GET: Preregistros/Create

        public IActionResult Create([FromQuery(Name = "TipoPreRegistro")] string tipoPreregistro)
        {           
            var _Tipopr = _context.TipoPreregistro.Where(x => x.TipoprId == Int32.Parse(tipoPreregistro)).First();

            var preRegistro = new Preregistros
            {
                PreregFechaCreacion = DateTime.Now,
                PreregFechaModificacion = DateTime.Now,
                Tipopr = _Tipopr,
                TipoprId = _Tipopr.TipoprId,
                EstprId = 1
            };

            LlenarDiccionarioUI(preRegistro);
            return View(preRegistro);
        }

        // POST: Preregistros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]  Preregistros preregistro, IFormFile formFile)
        {
            if (formFile == null && preregistro.TipoprId == 2)
            {
                ModelState.AddModelError("PreregAdjunto", "Por favor adjunte la hoja de vida");
                return View(preregistro);
            }

            if ((preregistro.PreregAreaProfesional == null) && preregistro.TipoprId == 2)
            {
                ModelState.AddModelError("PreregAreaProfesional", "Área profesional es un campo obligatorio");
                LlenarDiccionarioUI(preregistro);
                return View(preregistro);
            }
            if ((preregistro.PreregTematica == null) && preregistro.TipoprId == 2)
            {
                ModelState.AddModelError("PreregTematica", "Temática es un campo obligatorio");
                LlenarDiccionarioUI(preregistro);
                return View(preregistro);
            }
           
            if (preregistro.TipoprId == 2)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);
                    preregistro.PreregAdjunto = memoryStream.ToArray();
                }
            }

            preregistro.PreregFechaCreacion = DateTime.Now;
            preregistro.PreregFechaModificacion = DateTime.Now;
            preregistro.EstprId = 1;

            _context.Add(preregistro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
     
        }

        private void LlenarDiccionarioUI(Preregistros preregistro)
        {

                ViewData["EstprId"] = new SelectList(_context.EstadoPrereg, "EstprId", "EstprNombre", preregistro.EstprId);
                ViewData["TipoprId"] = new SelectList(_context.TipoPreregistro, "TipoprId", "TipoprNombre", preregistro.TipoprId);

        }

        // GET: Preregistros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preregistros = await _context.Preregistros
                                    .Include(p => p.Evaluacion)
                                    .ThenInclude(eval => eval.Usr)
                                    .SingleOrDefaultAsync(m => m.PreregId == id);                                   
                                    
                                  
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
 
        public async Task<IActionResult> Edit(int id, [FromForm] Preregistros preregistro, IFormFile formFile)
        {
            if (id != preregistro.PreregId)
            {
                return NotFound();
            }
            var preregInicial = await _context.Preregistros
                                  .Include(p => p.Evaluacion)
                                  .ThenInclude(eval => eval.Usr)
                                  .SingleOrDefaultAsync(m => m.PreregId == id);

            preregInicial.PreregFechaModificacion = DateTime.Now;
            preregInicial.EstprId = 2;
            preregInicial.PreregNombres = preregistro.PreregNombres;
            preregInicial.PreregApellidos = preregistro.PreregApellidos;
            preregInicial.PreregAreaProfesional = preregistro.PreregAreaProfesional;
            preregInicial.PreregEmail = preregistro.PreregEmail;
            preregInicial.PreregIdentificacion = preregistro.PreregIdentificacion;
            preregInicial.PreregTematica = preregistro.PreregTematica;

            //if (!ModelState.IsValid)
            //{
            //    var modelErrors = new List<string>();
            //    foreach (var modelState in ModelState.Values)
            //    {
            //        foreach (var modelError in modelState.Errors)
            //        {
            //            modelErrors.Add(modelError.ErrorMessage);
            //        }
            //    }
            //    Console.WriteLine(modelErrors.ToString());
            //}

            if (formFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);
                    preregInicial.PreregAdjunto = memoryStream.ToArray();
                }
            }

            if (ModelState.IsValid)
            {
            
            try
                {
                    _context.Update(preregInicial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreregistrosExists(preregInicial.PreregId))
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
            LlenarDiccionarioUI(preregistro);
            return View(preregistro);
        }

        // GET: Preregistros/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var preregistros = await _context.Preregistros
        //        .Include(p => p.Estpr)
        //        .Include(p => p.Tipopr)
        //        .SingleOrDefaultAsync(m => m.PreregId == id);
        //    if (preregistros == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(preregistros);
        //}

        // POST: Preregistros/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var preregistros = await _context.Preregistros.SingleOrDefaultAsync(m => m.PreregId == id);
        //    _context.Preregistros.Remove(preregistros);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

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
