using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelo.Models
{
    public class BuscarViewModel
    {
        
        [Required(ErrorMessage = "Ingrese su identificación")]
        [Display(Name = "Identificación")]
        [StringLength(60, MinimumLength = 3)]
        public string PreregIdentificacion { get; set; }

        [Required(ErrorMessage = "Nombre(s) es un campo obligatorio")]
        [Display(Name = "Nombre(s)")]
        [StringLength(60, MinimumLength = 3)]
        public string PreregNombres { get; set; }
        [Required(ErrorMessage = "Apellidos(s) es un campo obligatorio")]
        [Display(Name = "Apellido(s)")]
        [StringLength(60, MinimumLength = 3)]
        public string PreregApellidos { get; set; }

    }
}
