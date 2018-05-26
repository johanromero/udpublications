using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Modelo.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Evaluacion = new HashSet<Evaluacion>();
        }

        public int UsrId { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string UsrNombre { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La contraseña debe ser de por lo menos 6 dígitos", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string UsrPassword { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public bool UsrActivo { get; set; }
        public int RolId { get; set; }

        public Roles Rol { get; set; }
        public ICollection<Evaluacion> Evaluacion { get; set; }
    }
}
