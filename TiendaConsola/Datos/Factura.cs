using System;
using System.Collections.Generic;

namespace TiendaConsola.Datos
{
    public class Factura
    {
        private int id;
        private Usuario usuario;
        private List<LineaFactura> lineas;
        private decimal subtotal;
        private decimal descuentoCliente;
        private decimal descuentoAdicional;
        private decimal total;
        private DateTime fecha;

        public Factura(int id, Usuario usuario, List<LineaFactura> lineas,
                       decimal subtotal, decimal descuentoCliente,
                       decimal descuentoAdicional, decimal total)
        {
            this.id = id;
            this.usuario = usuario;
            this.lineas = lineas;
            this.subtotal = subtotal;
            this.descuentoCliente = descuentoCliente;
            this.descuentoAdicional = descuentoAdicional;
            this.total = total;
            this.fecha = DateTime.Now;
        }

        public int GetId() { return id; }
        public Usuario GetUsuario() { return usuario; }
        public List<LineaFactura> GetLineas() { return lineas; }
        public decimal GetSubtotal() { return subtotal; }
        public decimal GetDescuentoCliente() { return descuentoCliente; }
        public decimal GetDescuentoAdicional() { return descuentoAdicional; }
        public decimal GetTotal() { return total; }
        public DateTime GetFecha() { return fecha; }
    }
}
