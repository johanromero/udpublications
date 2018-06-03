using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelo.Models
{
    public partial class EstadoPrereg
    {
        public EstadoPrereg()
        {
            Preregistros = new HashSet<Preregistros>();
        }

       
        [Key]
        public int EstprId { get; set; }
        public string EstprNombre { get; set; }

        public ICollection<Preregistros> Preregistros { get; set; }
    }
}
