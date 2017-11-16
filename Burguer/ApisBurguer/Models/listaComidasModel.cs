using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApisBurguer.Models
{
    public class listaComidasModel
    {
        public int idComida { get; set; }
        public string nombreComida { get; set; }
        public string descripcionComida { get; set; }
        public int precioComida { get; set; }
        public string imagenComida { get; set; }
    }
}