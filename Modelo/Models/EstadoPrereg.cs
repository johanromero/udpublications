using System;
using System.Collections.Generic;

namespace Modelo.Models
{
    public partial class EstadoPrereg
    {
        public EstadoPrereg()
        {
            Preregistros = new HashSet<Preregistros>();
        }

        public int EstprId { get; set; }
        public string EstprNombre { get; set; }

        public ICollection<Preregistros> Preregistros { get; set; }
    }
}
