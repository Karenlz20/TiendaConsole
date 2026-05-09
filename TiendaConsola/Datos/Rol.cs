namespace TiendaConsola.Datos
{
    public class Rol
    {
        private int id;
        private string nombre;

        public Rol(int id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }

        public int GetId() { return id; }
        public void SetId(int id) { this.id = id; }

        public string GetNombre() { return nombre; }
        public void SetNombre(string nombre) { this.nombre = nombre; }
    }
}
