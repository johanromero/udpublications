using System;
using System.Collections.Generic;

namespace Modelo.Models
{
    public partial class Preregistros
    {
        public Preregistros()
        {
            Evaluacion = new HashSet<Evaluacion>();
        }

        public int PreregId { get; set; }
        public string PreregIdentificacion { get; set; }
        public string PreregNombres { get; set; }
        public string PreregApellidos { get; set; }
        public string PreregEmail { get; set; }
        public int CvId { get; set; }
        public string PreregTematica { get; set; }
        public string PreregAreaProfesional { get; set; }
        public DateTime PreregFechaCreacion { get; set; }
        public DateTime? PreregFechaModificacion { get; set; }
        public int TipoprId { get; set; }
        public int? EstprId { get; set; }

        public Cv Cv { get; set; }
        public EstadoPrereg Estpr { get; set; }
        public TipoPreregistro Tipopr { get; set; }
        public ICollection<Evaluacion> Evaluacion { get; set; }
    }
}
