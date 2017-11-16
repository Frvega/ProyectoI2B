using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApisBurguer.Models
{
    public class listaBebidasModel
    {
        public int idBebida { get; set; }
        public string nombreBebida { get; set; }
        public string descripcionBebida { get; set; }
        public int precioBebida { get; set; }
        public string imagenBebida { get; set; }
    }
}