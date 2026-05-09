using System;
using System.Collections.Generic;
using TiendaConsola.Datos;
using TiendaConsola.Negocio;

namespace TiendaConsola.Presentacion
{
    public class PresentacionCliente
    {
        private Inventario inventario;
        private Carrito carrito;
        private Ventas ventas;
        private GeneradorFactura genFactura;

        public PresentacionCliente(Inventario inventario, Carrito carrito,
                                   Ventas ventas, GeneradorFactura genFactura)
        {
            this.inventario = inventario;
            this.carrito = carrito;
            this.ventas = ventas;
            this.genFactura = genFactura;
        }

        public void MenuCliente(Usuario usuario)
        {
            string opcion = "";
            while (opcion != "4")
            {
                Console.WriteLine("\n--- MENU CLIENTE (" + usuario.GetNombre() + ") ---");
                Console.WriteLine("1. Ver productos disponibles");
                Console.WriteLine("2. Realizar compra");
                Console.WriteLine("3. Ver mis facturas");
                Console.WriteLine("4. Salir");
                Console.Write("Opcion: ");
                opcion = Console.ReadLine();

                if (opcion == "1") VerProductosDisponibles();
                else if (opcion == "2") RealizarCompra(usuario);
                else if (opcion == "3") VerMisFacturas(usuario);
                else if (opcion != "4") Console.WriteLine("Opcion invalida.");
            }
        }

        public void VerProductosDisponibles()
        {
            Console.WriteLine("\n--- PRODUCTOS DISPONIBLES ---");
            List<Producto> productos = inventario.ObtenerProductos();
            bool hayAlguno = false;
            foreach (Producto p in productos)
            {
                int stock = inventario.GetStock(p.GetId());
                if (stock > 0)
                {
                    Console.WriteLine("ID: " + p.GetId()
                        + "  " + p.GetNombre()
                        + "  $" + p.GetPrecio()
                        + "  Stock: " + stock);
                    hayAlguno = true;
                }
            }
            if (!hayAlguno)
                Console.WriteLine("No hay productos disponibles.");
        }

        public void RealizarCompra(Usuario usuario)
        {
            carrito.VaciarCarrito();

            string continuar = "s";
            while (continuar == "s")
            {
                VerProductosDisponibles();
                Console.Write("ID del producto (0 para terminar): ");
                int prodId = int.Parse(Console.ReadLine());
                if (prodId == 0) break;

                if (!inventario.HayDisponibles(prodId))
                {
                    Console.WriteLine("Producto no disponible.");
                    continue;
                }

                // Obtener referencia al producto
                Producto producto = null;
                List<UnidadProducto> unidades = inventario.BuscarPorId(prodId);
                if (unidades.Count > 0)
                    producto = unidades[0].GetProducto();

                if (producto == null)
                {
                    Console.WriteLine("Producto no encontrado.");
                    continue;
                }

                int stock = inventario.GetStock(prodId);
                Console.Write("Cantidad (max " + stock + "): ");
                int cantidad = int.Parse(Console.ReadLine());

                if (cantidad <= 0 || cantidad > stock)
                {
                    Console.WriteLine("Cantidad invalida.");
                    continue;
                }

                carrito.AgregarItem(producto, cantidad);
                Console.WriteLine("Agregado al carrito.");

                Console.Write("Agregar otro producto? (s/n): ");
                continuar = Console.ReadLine();
            }

            if (carrito.EstaVacio())
            {
                Console.WriteLine("Carrito vacio. Compra cancelada.");
                return;
            }

            MostrarResumenCompra(usuario.GetCliente());

            if (ConfirmarOCancelar())
            {
                try
                {
                    Factura f = genFactura.GenerarFactura(usuario, carrito, usuario.GetCliente(), inventario);
                    ventas.AgregarFactura(f);
                    carrito.VaciarCarrito();
                    genFactura.ImprimirFactura(f);
                    Console.WriteLine("Compra realizada con exito.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Compra cancelada.");
            }
        }

        public void MostrarResumenCompra(Cliente cliente)
        {
            Console.WriteLine("\n--- RESUMEN DE COMPRA ---");
            foreach (ItemCarrito item in carrito.ObtenerItems())
            {
                Console.WriteLine(item.GetProducto().GetNombre()
                    + "  x" + item.GetCantidad()
                    + "  $" + item.GetPrecioUnitario()
                    + "  Subtotal: $" + item.GetSubtotal());
            }

            decimal subtotal = carrito.CalcularSubtotal();
            decimal descBase = carrito.CalcularDescBase(cliente);
            decimal descAdicional = carrito.CalcularDescAdicional(cliente);
            decimal total = carrito.CalcularTotal(cliente);

            Console.WriteLine("Subtotal            : $" + subtotal);
            if (descBase > 0)
                Console.WriteLine("Descuento base      : $" + descBase);
            if (descAdicional > 0)
                Console.WriteLine("Descuento adicional : $" + descAdicional);
            Console.WriteLine("Total               : $" + total);
        }

        public bool ConfirmarOCancelar()
        {
            Console.Write("Confirmar compra? (s/n): ");
            string resp = Console.ReadLine();
            return resp == "s";
        }

        private void VerMisFacturas(Usuario usuario)
        {
            Console.WriteLine("\n--- MIS FACTURAS ---");
            List<Factura> facturas = ventas.ObtenerPorUsuario(usuario.GetId());
            if (facturas.Count == 0)
            {
                Console.WriteLine("No tiene facturas.");
                return;
            }
            foreach (Factura f in facturas)
            {
                Console.WriteLine("Factura #" + f.GetId()
                    + "  Fecha: " + f.GetFecha().ToString("dd/MM/yyyy HH:mm")
                    + "  Total: $" + f.GetTotal());
            }

            Console.Write("Ver detalle (numero de factura, 0 para omitir): ");
            int fid = int.Parse(Console.ReadLine());
            if (fid > 0)
            {
                Factura factura = ventas.BuscarFactura(fid);
                if (factura != null && factura.GetUsuario().GetId() == usuario.GetId())
                    genFactura.ImprimirFactura(factura);
                else
                    Console.WriteLine("Factura no encontrada.");
            }
        }
    }
}
