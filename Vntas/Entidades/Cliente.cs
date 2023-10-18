using System;
using System.Collections.Generic;

namespace Vntas.Entidades
{
    public partial class Cliente
    {
        public Cliente()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = null!;
        public int TelefonoCliente { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
