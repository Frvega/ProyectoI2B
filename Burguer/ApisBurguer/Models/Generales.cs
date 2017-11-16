using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApisBurguer.Models
{
    public class Generales
    {
        public int idPromocion { get; set; }
        public string descripcionPromocion { get; set; }
        public int costoPromocion { get; set; }
        public string categoriaPromocion { get; set; }
    }
}