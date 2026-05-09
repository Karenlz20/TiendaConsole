namespace TiendaConsola.Datos
{
    // Estado: "DISPONIBLE", "VENDIDA"
    public class UnidadProducto
    {
        private string codigo;
        private string estado;
        private Producto producto;

        public UnidadProducto(string codigo, Producto producto)
        {
            this.codigo = codigo;
            this.producto = producto;
            this.estado = "DISPONIBLE";
        }

        public string GetCodigo() { return codigo; }
        public Producto GetProducto() { return producto; }
        public string GetEstado() { return estado; }

        public void MarcarVendida() { estado = "VENDIDA"; }
        public bool EstaDisponible() { return estado == "DISPONIBLE"; }
    }
}
