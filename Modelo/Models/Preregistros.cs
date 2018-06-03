using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelo.Models
{
    public partial class Preregistros
    {
        public Preregistros()
        {
            Evaluacion = new HashSet<Evaluacion>();
        }

      
        [Key]
        public int PreregId { get; set; }
    
      
        [Required(ErrorMessage = "La identificación es un campo obligatorio")]
        [Display(Name = "Identificación")]
        [StringLength(60, MinimumLength = 3) ]
        public string PreregIdentificacion { get; set; }
        
        [Required(ErrorMessage = "Nombre(s) es un campo obligatorio")]
        [Display(Name = "Nombre(s)")]
        [StringLength(60, MinimumLength = 3)]
        public string PreregNombres { get; set; }
        [Required (ErrorMessage = "Apellidos(s) es un campo obligatorio")]
        [Display(Name = "Apellido(s)")]
        [StringLength(60, MinimumLength = 3)]

        public string PreregApellidos { get; set; }
        [Required (ErrorMessage = "Email es un campo obligatorio")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage ="Ingrese un email válido")]
        [StringLength(60, MinimumLength = 3)]
        public string PreregEmail { get; set; }
        [Required(ErrorMessage = "Curriculum Vitae es un campo obligatorio")]
        [Display(Name = "Curriculum Vitae (Hoja de Vida)")]
        public byte[] PreregAdjunto { get; set; }
        [Required(ErrorMessage = "Temática es un campo obligatorio")]
        [Display(Name = "Temática")]
        [StringLength(60, MinimumLength = 3) ]

        public string PreregTematica { get; set; }
        [Required(ErrorMessage = "Área profesional es un campo obligatorio")]
        [Display(Name = "Área profesional")]
        [StringLength(60, MinimumLength = 3)]

        public string PreregAreaProfesional { get; set; }
       
        public DateTime PreregFechaCreacion { get; set; }        
       
        public DateTime? PreregFechaModificacion { get; set; }       
       
        public int TipoprId { get; set; }
        public int? EstprId { get; set; }
        public EstadoPrereg Estpr { get; set; }
        public TipoPreregistro Tipopr { get; set; }
        public ICollection<Evaluacion> Evaluacion { get; set; }
        [NotMapped]
        public IFormFile files { get; set; }
    }
}
