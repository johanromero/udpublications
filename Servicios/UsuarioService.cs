using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Modelo.Models;
using Repositorio;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios
{
    public class UsuarioService
    {
        public static async Task<List<Usuario>> ObtenerListaUsuario(UDPUBLISHContext _context)
        {
            return await _context.Usuario.Include(u => u.Rol).ToListAsync();
        }
    }
}
