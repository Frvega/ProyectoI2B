using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApisBurguer.Models
{
    public class promocionesGenerales
    {
        public List<promocionesInfatiles> Infantiles { get; set; }
        public List<promocionesReuniones> Reuniones { get; set; }
        public List<Generales> Generales { get; set; }
    }
}