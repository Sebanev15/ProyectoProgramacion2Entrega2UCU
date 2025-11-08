using Library.interfaces;

namespace Library.abstractions
{
    public abstract class UsuarioBase
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
    
        public IGestionSistema GestionSistema = new GestionSistema();
        public bool EstaSuspendido { get; set; }

        public UsuarioBase(string esteNombre, string esteCorreo, string esteTelefono)
        {
            this.Nombre = esteNombre;
            this.Correo = esteCorreo;
            this.Telefono = esteTelefono;
            this.EstaSuspendido = false;
        }

        public void Suspender()
        {
            this.EstaSuspendido = true;
        }

        public void Reactivar()
        {
            this.EstaSuspendido = false;
        }
    }
}