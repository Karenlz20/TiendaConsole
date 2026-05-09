using System;
using System.Collections.Generic;
using TiendaConsola.Datos;

namespace TiendaConsola.Negocio
{
    public class GeneradorFactura
    {
        private static int nextId = 1;

        public Factura GenerarFactura(Usuario usuario, Carrito carrito, Cliente cliente, Inventario inventario)
        {
            List<LineaFactura> lineas = carrito.GenerarLineas(inventario);
            decimal subtotal = carrito.CalcularSubtotal();
            decimal descBase = cliente.CalcDescBase(subtotal);
            decimal descAdicional = cliente.CalcDescAdicional(subtotal);
            decimal total = subtotal - descBase - descAdicional;

            Factura f = new Factura(nextId, usuario, lineas, subtotal, descBase, descAdicional, total);
            nextId++;
            return f;
        }

        public void ImprimirFactura(Factura f)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("                  FACTURA DE VENTA");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Factura N: " + f.GetId());
            Console.WriteLine("Fecha    : " + f.GetFecha().ToString("dd/MM/yyyy HH:mm"));
            Console.WriteLine("Cliente  : " + f.GetUsuario().GetNombre());
            Console.WriteLine("Tipo     : " + f.GetUsuario().GetCliente().GetTipoCliente());
            Console.WriteLine("--------------------------------------------------");

            foreach (LineaFactura linea in f.GetLineas())
            {
                Console.WriteLine(linea.GetUnidad().GetProducto().GetNombre()
                    + " [" + linea.GetUnidad().GetCodigo() + "]"
                    + "  $" + linea.GetPrecioUnitario());
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Subtotal            : $" + f.GetSubtotal());
            if (f.GetDescuentoCliente() > 0)
                Console.WriteLine("Descuento base      : $" + f.GetDescuentoCliente());
            if (f.GetDescuentoAdicional() > 0)
                Console.WriteLine("Descuento adicional : $" + f.GetDescuentoAdicional());
            Console.WriteLine("TOTAL               : $" + f.GetTotal());
            Console.WriteLine("--------------------------------------------------");
        }
    }
}
