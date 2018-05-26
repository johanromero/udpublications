using System;
using System.Collections.Generic;

namespace Modelo.Models
{
    public partial class Permisos
    {
        public int PermId { get; set; }
        public string PermModulo { get; set; }
        public int RolId { get; set; }

        public Roles Rol { get; set; }
    }
}
