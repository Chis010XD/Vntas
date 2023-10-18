using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Vntas.Clases;
using Vntas.Entidades;

namespace Vntas.Controllers
{
    [ApiController]
    [Route("api/ventas")]
    public class VentController:ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VentController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]

        public  async Task<ActionResult<List<Vnt>>> GetTabla()  //retorno de lista de tipo Vnt de la clase 
        {


            var TablaJoins =  await(from Cliente in _context.Clientes
                             join Producto in _context.Productos on Cliente.IdCliente equals Producto.IdCliente
                             join Factura in _context.Facturas on Producto.IdProducto equals Factura.IdProducto 
                             select new Vnt()  // objeto de tipo vnt de la clase 
                             {
                                 NombreCliente = Cliente.NombreCliente,
                                 NombreProducto = Producto.NombreProducto,
                                 TotalFactura = Factura.TotalFactura
                             }).ToListAsync();

            /*var lista=new List<Vnt>();   ///creacion de una lista de esta clase de tipo vnt 

            foreach(var x in TablaJoins)  //recorremos la tabla joins de la tablajoins 
            {
                var resp=new Vnt();   // creacion de un objeto
                resp.NombreCliente=x.NombreCliente;               // asigancion de propiedades, valores
                resp.NombreProducto = x.NombreProducto;
                resp.TotalFactura = x.TotalFactura;

                lista.Add(resp);   //aniade objeto vnt a la lista de vnt 
            }*/

            return TablaJoins;

        }
    }
}



