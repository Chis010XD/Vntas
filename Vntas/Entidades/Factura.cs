using System;
using System.Collections.Generic;

namespace Vntas.Entidades
{
    public partial class Factura
    {
        public int IdFactura { get; set; }
        public DateTime FechaFactura { get; set; }
        public decimal TotalFactura { get; set; }
        public int? IdProducto { get; set; }

        public virtual Producto? IdProductoNavigation { get; set; }
    }
}
