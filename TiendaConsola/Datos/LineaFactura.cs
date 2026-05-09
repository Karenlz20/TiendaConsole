namespace TiendaConsola.Datos
{
    public class LineaFactura
    {
        private UnidadProducto unidad;
        private int cantidad;
        private decimal precioUnitario;

        public LineaFactura(UnidadProducto unidad, int cantidad, decimal precioUnitario)
        {
            this.unidad = unidad;
            this.cantidad = cantidad;
            this.precioUnitario = precioUnitario;
        }

        public UnidadProducto GetUnidad() { return unidad; }
        public int GetCantidad() { return cantidad; }
        public decimal GetPrecioUnitario() { return precioUnitario; }

        public decimal GetSubtotalLinea()
        {
            return precioUnitario * cantidad;
        }
    }
}
