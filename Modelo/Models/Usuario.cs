using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelo.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Evaluacion = new HashSet<Evaluacion>();
        }

        
        [Key]
        public int UsrId { get; set; }
        [Required]
        [Display(Name = "Usuario")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "El nombre del usuario es obligatorio")]

        public string UsrNombre { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        [StringLength(15, ErrorMessage = "La contraseña debe ser de por lo menos 6 dígitos", MinimumLength = 6)]
        [DataType(DataType.Password)]


        public string UsrPassword { get; set; }
   
        [Display(Name = "Es un usuario activo?")]
        [Required]
        public bool UsrActivo { get; set; }

        [Display(Name = "Rol")]
        [Required]
        public int RolId { get; set; }

        public Roles Rol { get; set; }
        public ICollection<Evaluacion> Evaluacion { get; set; }
    }
}
