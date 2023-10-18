using System;
using System.Collections.Generic;

namespace Vntas.Entidades
{
    public partial class Producto
    {
        public Producto()
        {
            Facturas = new HashSet<Factura>();
        }

        public int IdProducto { get; set; }
        public int? IdCliente { get; set; }
        public string NombreProducto { get; set; } = null!;
        public decimal PrecioProducto { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
