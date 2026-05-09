using System.Collections.Generic;

namespace TiendaConsola.Datos
{
    public class Usuario
    {
        private int id;
        private string nombre;
        private string username;
        private string password;
        private List<Rol> roles;
        private Cliente cliente;

        public Usuario(int id, string nombre, string username, string password, Cliente cliente)
        {
            this.id = id;
            this.nombre = nombre;
            this.username = username;
            this.password = password;
            this.cliente = cliente;
            this.roles = new List<Rol>();
        }

        public int GetId() { return id; }
        public string GetNombre() { return nombre; }
        public string GetUsername() { return username; }
        public Cliente GetCliente() { return cliente; }
        public List<Rol> GetRoles() { return roles; }

        public void AgregarRol(Rol rol) { roles.Add(rol); }

        public bool TieneRol(string nombre)
        {
            foreach (Rol r in roles)
            {
                if (r.GetNombre() == nombre)
                    return true;
            }
            return false;
        }

        public bool ValidarCredenciales(string password)
        {
            return this.password == password;
        }
    }
}
