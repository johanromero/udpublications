using System;
using System.Collections.Generic;

namespace Modelo.Models
{
    public partial class TipoPreregistro
    {
        public TipoPreregistro()
        {
            Preregistros = new HashSet<Preregistros>();
        }

        public int TipoprId { get; set; }
        public string TipoprNombre { get; set; }

        public ICollection<Preregistros> Preregistros { get; set; }
    }
}
