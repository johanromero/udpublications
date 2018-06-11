using System;
using System.Collections.Generic;
using System.Text;
using Modelo.Models;
using Repositorio;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Servicios
{
    public class ReportesServices
    {
        public static  List<Reportes> ObtenerReporteXFechas(UDPUBLISHContext _context, DateTime fechaIni, DateTime fechaFin)
        {
            var qResult = (from pre in _context.Preregistros
                           join est in _context.EstadoPrereg
                           on pre.EstprId equals est.EstprId
                           join tip in _context.TipoPreregistro
                           on pre.TipoprId equals tip.TipoprId
                           join eval in _context.Evaluacion
                           on pre.PreregId equals eval.PreregId into gj
                           from x in gj.DefaultIfEmpty()
                           select new Reportes
                           {
                               PreregApellidos = pre.PreregApellidos,
                               PreregAreaProfesional = pre.PreregAreaProfesional,
                               preregEditCount = pre.preregEditCount,
                               PreregEmail = pre.PreregEmail,
                               PreregFechaCreacion = pre.PreregFechaCreacion,
                               PreregFechaModificacion = pre.PreregFechaModificacion,
                               PreregIdentificacion = pre.PreregIdentificacion,
                               PreregNombres = pre.PreregNombres,
                               PreregTematica = pre.PreregTematica,
                               TipoprNombre = tip.TipoprNombre,
                               EstprNombre = est.EstprNombre,
                               EvalObservacion = gj.FirstOrDefault().EvalObservacion
                           }).ToList();

            return qResult;
        }
    }
}
