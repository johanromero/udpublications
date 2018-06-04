using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelo.Models
{
    public partial class Evaluacion
    {
       
        [Key]
        public int EvalId { get; set; }
        public int PreregId { get; set; }
        [Required(ErrorMessage = "Observación es un campo obligatorio")]
        [Display(Name = "Observación")]
        [StringLength(60, MinimumLength = 20, ErrorMessage ="Las observaciones son de mínimo 20 caracteres")]
        public string EvalObservacion { get; set; }
        [Display(Name = "Evaluador")]
        public int UsrId { get; set; }

        public Preregistros Prereg { get; set; }
        public Usuario Usr { get; set; }
    }
}
