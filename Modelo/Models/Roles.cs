using System;
using System.Collections.Generic;

namespace Modelo.Models
{
    public partial class Roles
    {
        public Roles()
        {
            Permisos = new HashSet<Permisos>();
            Usuario = new HashSet<Usuario>();
        }

        public int RolId { get; set; }
        public string RolNombre { get; set; }

        public ICollection<Permisos> Permisos { get; set; }
        public ICollection<Usuario> Usuario { get; set; }
    }
}
