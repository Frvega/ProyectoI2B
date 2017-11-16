using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApisBurguer.Models
{
    public class promocionContrada
    {
        public int idPromocionContratada { get; set; }
        public int idPromocion { get; set; }
        public string nombreCliente { get; set; }
        public string apellidoCliente { get; set; }
        public string direccionCliente { get; set; }
        public string telefonoCliente { get; set; }
        public string fechaPedido { get; set; }
        public string fechaEntrega { get; set; }
    }
}