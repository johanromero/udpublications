using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelo.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Repositorio;
using Servicios;

namespace udpublications.Controllers
{
    public class ReportesController : Controller
    {
        private readonly UDPUBLISHContext _context;
        private IHostingEnvironment _hostingEnvironment;


        public ReportesController(UDPUBLISHContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR,EVALUADOR,COMITÉ")]
        public  FileResult Exportar([FromForm] ReportesViewModel rep)
        {
            var qResult = ReportesServices.ObtenerReporteXFechas(_context, rep.fechaFin, rep.fechaFin);
            string sFileName = @"ReportePublicacionesUD.xlsx";
            var dt = ExcelServices.ToDataTable<Reportes>(qResult);
            byte[] reportDocument = ExcelServices.RenderDataTableToExcel(dt).ToArray();

            return File(reportDocument, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

            //string sWebRootFolder = _hostingEnvironment.WebRootPath;
            //FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));


        }

    }
}