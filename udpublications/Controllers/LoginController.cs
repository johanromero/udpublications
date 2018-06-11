using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelo.Models;
using Repositorio;

namespace udpublications.Controllers
{
    public class LoginController : Controller
    {
        private readonly UDPUBLISHContext _context;

        public LoginController(UDPUBLISHContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login([FromForm] LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _context.Usuario.Include(u => u.Rol)
                            .SingleOrDefaultAsync(m => 
                                            (m.UsrNombre == loginModel.Username
                                                && m.UsrPassword == loginModel.Password));

                var userExist = usuario != null; // TODO Validate the username and the password with your own logic
                if (!userExist)
                {
                    ModelState.AddModelError("Username", "Usuario o contraseña inválidos");
                    return View("Index",loginModel);
                }

                if (!usuario.UsrActivo)
                {
                    ModelState.AddModelError("Username", "Usuario inactivo");
                    return View("Index", loginModel);
                }

                CreateClaims(usuario);

                return RedirectToAction("Index", "Home");
            }

            return View(loginModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index), "Home");
        }

         
        private async void CreateClaims(Usuario usuario)
        {

            // Create the identity from the user info
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.UsrId.ToString().Trim()));
            identity.AddClaim(new Claim(ClaimTypes.Name, usuario.UsrNombre.Trim()));
            identity.AddClaim(new Claim(ClaimTypes.Role, usuario.Rol.RolNombre.Trim()));

            // Authenticate using the identity
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        }
    }
}