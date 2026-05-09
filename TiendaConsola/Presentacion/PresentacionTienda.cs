using System;
using TiendaConsola.Datos;
using TiendaConsola.Negocio;

namespace TiendaConsola.Presentacion
{
    public class PresentacionTienda
    {
        private PresentacionAdmin presAdmin;
        private PresentacionCliente presCliente;
        private GestorUsuarios gestorUsuarios;
        private Sesion sesion;
        private Ventas ventas;

        public PresentacionTienda()
        {
            Inventario inventario = new Inventario();
            gestorUsuarios = new GestorUsuarios();
            sesion = new Sesion();
            ventas = new Ventas();
            GeneradorFactura genFactura = new GeneradorFactura();
            Carrito carrito = new Carrito();

            presAdmin = new PresentacionAdmin(inventario, gestorUsuarios);
            presCliente = new PresentacionCliente(inventario, carrito, ventas, genFactura);
        }

        public void Iniciar()
        {
            string resp = "s";
            while (resp == "s")
            {
                MostrarLogin();
                if (sesion.EstaAutenticado())
                {
                    DerivarSegunRol(sesion.GetUsuarioActual());
                    CerrarSesion();
                }
                Console.Write("Iniciar sesion nuevamente? (s/n): ");
                resp = Console.ReadLine();
            }
            CerrarTienda();
        }

        public void MostrarLogin()
        {
            Console.WriteLine("\nTIENDA CONSOLA");
            PedirCredenciales();
        }

        public void PedirCredenciales()
        {
            Console.Write("Usuario: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            Usuario usuario = gestorUsuarios.Autenticar(username, password);
            if (usuario == null)
                Console.WriteLine("Credenciales incorrectas.");
            else
            {
                sesion.IniciarSesion(usuario);
                Console.WriteLine("Bienvenido, " + usuario.GetNombre() + "!");
            }
        }
        

        public void DerivarSegunRol(Usuario usuario)
        {
            if (usuario.TieneRol("Admin") && usuario.TieneRol("Cliente"))
            {
                Console.WriteLine("\nTiene multiples roles:");
                Console.WriteLine("1. Administrador");
                Console.WriteLine("2. Cliente");
                Console.Write("Opcion: ");
                string op = Console.ReadLine();

                if (op == "1")
                {
                    sesion.SetRolActivo(usuario.GetRoles()[0]);
                    presAdmin.MenuAdmin();
                }
                else
                {
                    sesion.SetRolActivo(usuario.GetRoles()[1]);
                    presCliente.MenuCliente(usuario);
                }
            }
            else if (usuario.TieneRol("Admin"))
            {
                presAdmin.MenuAdmin();
            }
            else if (usuario.TieneRol("Cliente"))
            {
                presCliente.MenuCliente(usuario);
            }
            else
            {
                Console.WriteLine("El usuario no tiene roles asignados.");
            }
        }

        public void CerrarSesion()
        {
            sesion.CerrarSesion();
            Console.WriteLine("Sesion cerrada.");
        }

        public void CerrarTienda()
        {
            Console.WriteLine("Hasta luego.");
        }
    }
}
