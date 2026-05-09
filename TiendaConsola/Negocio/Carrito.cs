using System;
using System.Collections.Generic;
using TiendaConsola.Datos;

namespace TiendaConsola.Negocio
{
    public class Carrito
    {
        private List<ItemCarrito> items;

        public Carrito()
        {
            items = new List<ItemCarrito>();
        }

        public void AgregarItem(Producto producto, int cantidad)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GetProducto().GetId() == producto.GetId())
                {
                    items[i].SetCantidad(items[i].GetCantidad() + cantidad);
                    return;
                }
            }
            items.Add(new ItemCarrito(producto, cantidad));
        }

        public void EliminarItem(int prodId)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GetProducto().GetId() == prodId)
                {
                    items.RemoveAt(i);
                    return;
                }
            }
        }

        public List<ItemCarrito> ObtenerItems() { return items; }

        public decimal CalcularSubtotal()
        {
            decimal total = 0;
            foreach (ItemCarrito item in items)
                total += item.GetSubtotal();
            return total;
        }

        public decimal CalcularDescBase(Cliente cliente)
        {
            return cliente.CalcDescBase(CalcularSubtotal());
        }

        public decimal CalcularDescAdicional(Cliente cliente)
        {
            return cliente.CalcDescAdicional(CalcularSubtotal());
        }

        public decimal CalcularTotal(Cliente cliente)
        {
            decimal subtotal = CalcularSubtotal();
            return subtotal - cliente.CalcTotalDescuento(subtotal);
        }

        public void VaciarCarrito() { items.Clear(); }

        public bool EstaVacio() { return items.Count == 0; }

        public List<LineaFactura> GenerarLineas(Inventario inventario)
        {
            List<LineaFactura> lineas = new List<LineaFactura>();

            foreach (ItemCarrito item in items)
            {
                for (int i = 0; i < item.GetCantidad(); i++)
                {
                    UnidadProducto unidad = inventario.TomarUnidadDisponible(item.GetProducto().GetId());
                    if (unidad == null)
                        throw new Exception("Sin stock: " + item.GetProducto().GetNombre());

                    lineas.Add(new LineaFactura(unidad, 1, item.GetPrecioUnitario()));
                }
            }
            return lineas;
        }
    }
}
