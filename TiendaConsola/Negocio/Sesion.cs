using TiendaConsola.Datos;

namespace TiendaConsola.Negocio
{
    public class Sesion
    {
        private Usuario usuarioActual;
        private Rol rolActivo;

        public void IniciarSesion(Usuario u)
        {
            usuarioActual = u;
            if (u.GetRoles().Count > 0)
                rolActivo = u.GetRoles()[0];
        }

        public void CerrarSesion()
        {
            usuarioActual = null;
            rolActivo = null;
        }

        public bool EstaAutenticado()
        {
            return usuarioActual != null;
        }

        public Usuario GetUsuarioActual() { return usuarioActual; }
        public Rol GetRolActivo() { return rolActivo; }

        public void SetRolActivo(Rol rol) { rolActivo = rol; }
    }
}
