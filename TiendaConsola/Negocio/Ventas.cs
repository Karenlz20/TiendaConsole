using System.Collections.Generic;
using TiendaConsola.Datos;

namespace TiendaConsola.Negocio
{
    public class Ventas
    {
        private List<Factura> facturas;

        public Ventas()
        {
            facturas = new List<Factura>();
        }

        public void AgregarFactura(Factura f) { facturas.Add(f); }

        public List<Factura> ObtenerFacturas() { return facturas; }

        public List<Factura> ObtenerPorUsuario(int usuarioId)
        {
            List<Factura> resultado = new List<Factura>();
            foreach (Factura f in facturas)
            {
                if (f.GetUsuario().GetId() == usuarioId)
                    resultado.Add(f);
            }
            return resultado;
        }

        public Factura BuscarFactura(int id)
        {
            foreach (Factura f in facturas)
            {
                if (f.GetId() == id)
                    return f;
            }
            return null;
        }
    }
}
