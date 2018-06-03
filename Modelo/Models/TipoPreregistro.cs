using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelo.Models
{
    public partial class TipoPreregistro
    {
        public TipoPreregistro()
        {
            Preregistros = new HashSet<Preregistros>();
        }

      
        [Key]
        public int TipoprId { get; set; }
        public string TipoprNombre { get; set; }

        public ICollection<Preregistros> Preregistros { get; set; }
    }
}
