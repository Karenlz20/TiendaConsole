using System;
using System.Collections.Generic;
using TiendaConsola.Datos;

namespace TiendaConsola.Negocio
{
    public class Inventario
    {
        private List<UnidadProducto> unidades;
        private int nextProductoId;

        public Inventario()
        {
            unidades = new List<UnidadProducto>();
            nextProductoId = 1;
            CargarDatos();
        }

        public void AgregarUnidad(UnidadProducto u)
        {
            unidades.Add(u);
        }

        public void EliminarUnidad(string codigo)
        {
            UnidadProducto encontrada = BuscarPorCodigo(codigo);
            if (encontrada != null)
                unidades.Remove(encontrada);
        }

        // Devuelve un Producto por cada tipo distinto
        public List<Producto> ObtenerProductos()
        {
            List<Producto> resultado = new List<Producto>();
            List<int> idsVistos = new List<int>();

            foreach (UnidadProducto u in unidades)
            {
                int pid = u.GetProducto().GetId();
                if (!idsVistos.Contains(pid))
                {
                    idsVistos.Add(pid);
                    resultado.Add(u.GetProducto());
                }
            }
            return resultado;
        }

        public int GetStock(int prodId)
        {
            int count = 0;
            foreach (UnidadProducto u in unidades)
            {
                if (u.GetProducto().GetId() == prodId && u.EstaDisponible())
                    count++;
            }
            return count;
        }

        public List<UnidadProducto> BuscarPorId(int prodId)
        {
            List<UnidadProducto> resultado = new List<UnidadProducto>();
            foreach (UnidadProducto u in unidades)
            {
                if (u.GetProducto().GetId() == prodId)
                    resultado.Add(u);
            }
            return resultado;
        }

        public List<Producto> BuscarPorNombre(string nombre)
        {
            List<Producto> resultado = new List<Producto>();
            foreach (Producto p in ObtenerProductos())
            {
                if (p.GetNombre().ToLower().Contains(nombre.ToLower()))
                    resultado.Add(p);
            }
            return resultado;
        }

        public UnidadProducto BuscarPorCodigo(string codigo)
        {
            foreach (UnidadProducto u in unidades)
            {
                if (u.GetCodigo() == codigo)
                    return u;
            }
            return null;
        }

        public UnidadProducto TomarUnidadDisponible(int prodId)
        {
            foreach (UnidadProducto u in unidades)
            {
                if (u.GetProducto().GetId() == prodId && u.EstaDisponible())
                {
                    u.MarcarVendida();
                    return u;
                }
            }
            return null;
        }

        public bool HayDisponibles(int prodId)
        {
            foreach (UnidadProducto u in unidades)
            {
                if (u.GetProducto().GetId() == prodId && u.EstaDisponible())
                    return true;
            }
            return false;
        }

        public Producto AgregarProducto(string nombre, string descripcion, decimal precio, int cantidad)
        {
            Producto p = new Producto(nextProductoId, nombre, descripcion, precio);
            nextProductoId++;
            for (int i = 1; i <= cantidad; i++)
            {
                string codigo = "P" + p.GetId() + "-U" + i;
                unidades.Add(new UnidadProducto(codigo, p));
            }
            return p;
        }

        public void EliminarProducto(int prodId)
        {
            List<UnidadProducto> aEliminar = new List<UnidadProducto>();
            foreach (UnidadProducto u in unidades)
            {
                if (u.GetProducto().GetId() == prodId)
                    aEliminar.Add(u);
            }
            foreach (UnidadProducto u in aEliminar)
                unidades.Remove(u);
        }

        public bool ActualizarProducto(int prodId, string nombre, string descripcion, decimal precio)
        {
            foreach (UnidadProducto u in unidades)
            {
                if (u.GetProducto().GetId() == prodId)
                {
                    u.GetProducto().SetNombre(nombre);
                    u.GetProducto().SetDescripcion(descripcion);
                    u.GetProducto().SetPrecio(precio);
                    return true;
                }
            }
            return false;
        }

        private void CargarDatos()
        {
            AgregarProducto("iPhone 15 Pro", "Smartphone Apple 256GB", 1200m, 3);
            AgregarProducto("Samsung Galaxy S24", "Smartphone Samsung 128GB", 850m, 5);
            AgregarProducto("MacBook Air M2", "Laptop Apple 8GB RAM", 1100m, 2);
            AgregarProducto("AirPods Pro", "Auriculares inalambricos Apple", 250m, 8);
            AgregarProducto("Cargador USB-C 65W", "Cargador rapido universal", 45m, 15);
        }
    }
}
