namespace TiendaConsola.Datos
{
    public class ItemCarrito
    {
        private Producto producto;
        private int cantidad;
        private decimal precioUnitario;

        public ItemCarrito(Producto producto, int cantidad)
        {
            this.producto = producto;
            this.cantidad = cantidad;
            this.precioUnitario = producto.GetPrecio();
        }

        public Producto GetProducto() { return producto; }
        public int GetCantidad() { return cantidad; }
        public void SetCantidad(int cantidad) { this.cantidad = cantidad; }
        public decimal GetPrecioUnitario() { return precioUnitario; }

        public decimal GetSubtotal()
        {
            return precioUnitario * cantidad;
        }
    }
}
