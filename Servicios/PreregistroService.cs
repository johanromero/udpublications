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
    public class PreregistroService
    {
        public static async Task<List<Preregistros>> ObtenerListadoPreregistros(UDPUBLISHContext _context)
        {
            return  await _context.Preregistros.Include(p => p.Estpr).Include(p => p.Tipopr).ToListAsync();
        }

        public static async Task<Preregistros> ObtenerPreregistroXId(UDPUBLISHContext _context, int id)
        {
           return await _context.Preregistros
              .Include(p => p.Estpr)
              .Include(p => p.Tipopr)
              .Include(p => p.Evaluacion)
              .ThenInclude(eval => eval.Usr)
              .SingleOrDefaultAsync(m => m.PreregId == id);

        }
    }
}
