using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelo.Models
{
    public class ReportesViewModel
    {

        [Display(Name = "Fecha de Inicio")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="Introduzca una fecha de inicio")]
        public DateTime fechaInicio { get; set; }
        [Display(Name = "Fecha de Fin")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Introduzca una fecha final")]
        public DateTime fechaFin { get; set; }
    }
}
