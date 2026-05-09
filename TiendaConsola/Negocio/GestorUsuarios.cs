using System.Collections.Generic;
using TiendaConsola.Datos;

namespace TiendaConsola.Negocio
{
    public class GestorUsuarios
    {
        private List<Usuario> usuarios;
        private int nextId;

        public GestorUsuarios()
        {
            usuarios = new List<Usuario>();
            nextId = 1;
            CargarDatos();
        }

        public Usuario Autenticar(string username, string password)
        {
            foreach (Usuario u in usuarios)
            {
                if (u.GetUsername() == username && u.ValidarCredenciales(password))
                    return u;
            }
            return null;
        }

        public void RegistrarUsuario(string nombre, string username, string password, string tipoCliente)
        {
            Cliente cliente;
            if (tipoCliente == "VIP")
                cliente = new ClienteVIP();
            else
                cliente = new ClienteRegular();

            Usuario u = new Usuario(nextId, nombre, username, password, cliente);
            nextId++;
            u.AgregarRol(new Rol(2, "Cliente"));
            usuarios.Add(u);
        }

        public void AgregarUsuario(Usuario u)
        {
            usuarios.Add(u);
        }

        public bool EliminarUsuario(int id)
        {
            for (int i = 0; i < usuarios.Count; i++)
            {
                if (usuarios[i].GetId() == id)
                {
                    usuarios.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public void ActualizarUsuario(int id, string nombre, string username, string tipoCliente)
        {
            for (int i = 0; i < usuarios.Count; i++)
            {
                if (usuarios[i].GetId() == id)
                {
                    Usuario viejo = usuarios[i];
                    Cliente cliente;
                    if (tipoCliente == "VIP")
                        cliente = new ClienteVIP();
                    else
                        cliente = new ClienteRegular();

                    Usuario nuevo = new Usuario(viejo.GetId(), nombre, username, "sin_cambio", cliente);
                    foreach (Rol r in viejo.GetRoles())
                        nuevo.AgregarRol(r);

                    usuarios[i] = nuevo;
                    return;
                }
            }
        }

        public Usuario BuscarPorUsername(string username)
        {
            foreach (Usuario u in usuarios)
            {
                if (u.GetUsername() == username)
                    return u;
            }
            return null;
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return usuarios;
        }

        private void CargarDatos()
        {
            Rol rolAdmin = new Rol(1, "Admin");
            Rol rolCliente = new Rol(2, "Cliente");

            Usuario admin = new Usuario(nextId, "Administrador", "admin", "admin123", new ClienteVIP());
            nextId++;
            admin.AgregarRol(rolAdmin);
            admin.AgregarRol(rolCliente);
            usuarios.Add(admin);

            Usuario juan = new Usuario(nextId, "Juan Perez", "juan", "pass123", new ClienteRegular());
            nextId++;
            juan.AgregarRol(rolCliente);
            usuarios.Add(juan);

            Usuario maria = new Usuario(nextId, "Maria Garcia", "maria", "pass456", new ClienteVIP());
            nextId++;
            maria.AgregarRol(rolCliente);
            usuarios.Add(maria);
        }
    }
}
