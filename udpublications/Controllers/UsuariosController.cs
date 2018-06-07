using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modelo.Models;
using Repositorio;

namespace udpublications.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UDPUBLISHContext _context;

       public UsuariosController(UDPUBLISHContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Index()
        {
            var qUsuario =  _context.Usuario.Include(u => u.Rol);
            return  View(await qUsuario.ToListAsync());
        }

        // GET: Usuarios/Details/5
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .Include(u => u.Rol)
                .SingleOrDefaultAsync(m => m.UsrId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        [Authorize(Roles = "ADMINISTRADOR")]
        public IActionResult Create()
        {
            ViewData["RolId"] = new SelectList(_context.Roles, "RolId", "RolNombre");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Create([Bind("UsrId,UsrNombre,UsrPassword,UsrActivo,RolId")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RolId"] = new SelectList(_context.Roles, "RolId", "RolNombre", usuario.RolId);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.SingleOrDefaultAsync(m => m.UsrId == id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["RolId"] = new SelectList(_context.Roles, "RolId", "RolNombre", usuario.RolId);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Edit(int id, [Bind("UsrId,UsrNombre,UsrPassword,UsrActivo,RolId")] Usuario usuario)
        {
            if (id != usuario.UsrId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.UsrId))
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
            ViewData["RolId"] = new SelectList(_context.Roles, "RolId", "RolNombre", usuario.RolId);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .Include(u => u.Rol)
                .SingleOrDefaultAsync(m => m.UsrId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuario.SingleOrDefaultAsync(m => m.UsrId == id);
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.UsrId == id);
        }
    }
}
