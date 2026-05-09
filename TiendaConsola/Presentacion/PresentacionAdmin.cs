using System;
using System.Collections.Generic;
using TiendaConsola.Datos;
using TiendaConsola.Negocio;

namespace TiendaConsola.Presentacion
{
    public class PresentacionAdmin
    {
        private Inventario inventario;
        private GestorUsuarios gestorUsuarios;

        public PresentacionAdmin(Inventario inventario, GestorUsuarios gestorUsuarios)
        {
            this.inventario = inventario;
            this.gestorUsuarios = gestorUsuarios;
        }

        public void MenuAdmin()
        {
            string opcion = "";
            while (opcion != "9")
            {
                Console.WriteLine("\n--- MENU ADMIN ---");
                Console.WriteLine("1. Listar productos");
                Console.WriteLine("2. Agregar producto");
                Console.WriteLine("3. Actualizar producto");
                Console.WriteLine("4. Eliminar producto");
                Console.WriteLine("5. Listar usuarios");
                Console.WriteLine("6. Agregar usuario");
                Console.WriteLine("7. Actualizar usuario");
                Console.WriteLine("8. Eliminar usuario");
                Console.WriteLine("9. Salir");
                Console.Write("Opcion: ");
                opcion = Console.ReadLine();

                if (opcion == "1") ListarProductos();
                else if (opcion == "2") AgregarProducto();
                else if (opcion == "3") ActualizarProducto();
                else if (opcion == "4") EliminarProducto();
                else if (opcion == "5") ListarUsuarios();
                else if (opcion == "6") AgregarUsuario();
                else if (opcion == "7") ActualizarUsuario();
                else if (opcion == "8") EliminarUsuario();
                else if (opcion != "9") Console.WriteLine("Opcion invalida.");
            }
        }

        public void ListarProductos()
        {
            Console.WriteLine("\n--- PRODUCTOS ---");
            List<Producto> productos = inventario.ObtenerProductos();
            if (productos.Count == 0)
            {
                Console.WriteLine("No hay productos.");
                return;
            }
            foreach (Producto p in productos)
            {
                Console.WriteLine("ID: " + p.GetId()
                    + "  Nombre: " + p.GetNombre()
                    + "  Precio: $" + p.GetPrecio()
                    + "  Stock: " + inventario.GetStock(p.GetId())
                    + "  Desc: " + p.GetDescripcion());
            }
        }

        public void AgregarProducto()
        {
            Console.WriteLine("\n--- AGREGAR PRODUCTO ---");
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Descripcion: ");
            string desc = Console.ReadLine();
            Console.Write("Precio: ");
            decimal precio = decimal.Parse(Console.ReadLine());
            Console.Write("Cantidad de unidades: ");
            int cantidad = int.Parse(Console.ReadLine());

            Producto p = inventario.AgregarProducto(nombre, desc, precio, cantidad);
            Console.WriteLine("Producto agregado. ID: " + p.GetId());
        }

        public void ActualizarProducto()
        {
            ListarProductos();
            Console.Write("\nID del producto a actualizar: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Nuevo nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Nueva descripcion: ");
            string desc = Console.ReadLine();
            Console.Write("Nuevo precio: ");
            decimal precio = decimal.Parse(Console.ReadLine());

            bool ok = inventario.ActualizarProducto(id, nombre, desc, precio);
            if (ok) Console.WriteLine("Producto actualizado.");
            else Console.WriteLine("Producto no encontrado.");
        }

        public void EliminarProducto()
        {
            ListarProductos();
            Console.Write("\nID del producto a eliminar: ");
            int id = int.Parse(Console.ReadLine());
            inventario.EliminarProducto(id);
            Console.WriteLine("Producto eliminado.");
        }

        public void ListarUsuarios()
        {
            Console.WriteLine("\n--- USUARIOS ---");
            List<Usuario> usuarios = gestorUsuarios.ObtenerUsuarios();
            if (usuarios.Count == 0)
            {
                Console.WriteLine("No hay usuarios.");
                return;
            }
            foreach (Usuario u in usuarios)
            {
                string roles = "";
                foreach (Rol r in u.GetRoles())
                    roles += r.GetNombre() + " ";

                Console.WriteLine("ID: " + u.GetId()
                    + "  Nombre: " + u.GetNombre()
                    + "  Username: " + u.GetUsername()
                    + "  Tipo: " + u.GetCliente().GetTipoCliente()
                    + "  Roles: " + roles);
            }
        }

        public void AgregarUsuario()
        {
            Console.WriteLine("\n--- AGREGAR USUARIO ---");
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Console.Write("Tipo (Regular/VIP): ");
            string tipo = Console.ReadLine();

            gestorUsuarios.RegistrarUsuario(nombre, username, password, tipo);
            Console.WriteLine("Usuario registrado.");
        }

        public void ActualizarUsuario()
        {
            ListarUsuarios();
            Console.Write("\nID del usuario a actualizar: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Nuevo nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Nuevo username: ");
            string username = Console.ReadLine();
            Console.Write("Tipo (Regular/VIP): ");
            string tipo = Console.ReadLine();

            gestorUsuarios.ActualizarUsuario(id, nombre, username, tipo);
            Console.WriteLine("Usuario actualizado.");
        }

        public void EliminarUsuario()
        {
            ListarUsuarios();
            Console.Write("\nID del usuario a eliminar: ");
            int id = int.Parse(Console.ReadLine());
            bool ok = gestorUsuarios.EliminarUsuario(id);
            if (ok) Console.WriteLine("Usuario eliminado.");
            else Console.WriteLine("Usuario no encontrado.");
        }
    }
}
