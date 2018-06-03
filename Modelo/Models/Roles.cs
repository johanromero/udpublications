using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelo.Models
{
    public partial class Roles
    {
        public Roles()
        {
            //Permisos = new HashSet<Permisos>();
            Usuario = new HashSet<Usuario>();
        }

       
        [Key]
        public int RolId { get; set; }
        public string RolNombre { get; set; }

        //public ICollection<Permisos> Permisos { get; set; }
        public ICollection<Usuario> Usuario { get; set; }
    }
}
