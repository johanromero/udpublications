using System;
using System.Collections.Generic;

namespace Modelo.Models
{
    public partial class Evaluacion
    {
        public int EvalId { get; set; }
        public int PreregId { get; set; }
        public string EvalObservacion { get; set; }
        public int UsrId { get; set; }

        public Preregistros Prereg { get; set; }
        public Usuario Usr { get; set; }
    }
}
