namespace TiendaConsola.Datos
{
    public class Producto
    {
        private int id;
        private string nombre;
        private string descripcion;
        private decimal precio;

        public Producto(int id, string nombre, string descripcion, decimal precio)
        {
            this.id = id;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio = precio;
        }

        public int GetId() { return id; }
        public void SetId(int id) { this.id = id; }

        public string GetNombre() { return nombre; }
        public void SetNombre(string nombre) { this.nombre = nombre; }

        public string GetDescripcion() { return descripcion; }
        public void SetDescripcion(string descripcion) { this.descripcion = descripcion; }

        public decimal GetPrecio() { return precio; }
        public void SetPrecio(decimal precio) { this.precio = precio; }
    }
}
