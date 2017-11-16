using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApisBurguer.Models
{
    public class resultadoValidacion
    {

        public int status { get; set; }
        public string message { get; set; }
        public int idPuesto { get; set; }
    }
}