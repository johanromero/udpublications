using System;
using System.Collections.Generic;

namespace Modelo.Models
{
    public partial class Cv
    {
        public Cv()
        {
            Preregistros = new HashSet<Preregistros>();
        }

        public int CvId { get; set; }
        public byte[] CvAdjunto { get; set; }

        public ICollection<Preregistros> Preregistros { get; set; }
    }
}
