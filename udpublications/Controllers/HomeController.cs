using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Models;
using Microsoft.EntityFrameworkCore;
using Modelo.Models;
using Repositorio;

namespace dockersample.Controllers
{
    public class HomeController : Controller
    {
        private readonly UDPUBLISHContext _context;

        public HomeController(UDPUBLISHContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Proyecto para la asignatura Ingeniería de Software II";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buscar([FromForm] BuscarViewModel bvm)
        {

            if (ModelState.IsValid)
            {
                var preregistro = await _context.Preregistros
                                    .SingleOrDefaultAsync(m =>
                                    (m.PreregIdentificacion == bvm.PreregIdentificacion
                                    && m.PreregNombres.Trim().ToLower().
                                            Contains(bvm.PreregNombres.Trim().ToLower())
                                    && m.PreregApellidos.Trim().ToLower().
                                            Contains(bvm.PreregApellidos.Trim().ToLower())));
                if (preregistro == null)
                {
                    ModelState.AddModelError("PreregIdentificacion", "No hay preregistros asociados a los datos ingresados.");
                    return View(bvm);
                }
                if(preregistro.EstprId == 2)
                    return RedirectToAction("Edit", "Preregistro", preregistro.PreregId);
                if(preregistro.EstprId == 3)
                    return RedirectToAction("Details", "Preregistro", preregistro.PreregId);

            }

            return View(bvm);
        }
    }
}
