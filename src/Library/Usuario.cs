using Library.interfaces;

namespace Library.abstractions
{
    /// <summary>
    /// Clase base para los usuarios del sistema.
    /// haciendo esta clase abstracta, se cumple con OCP (Open/Closed Principle) ya que permitimos extender a nuevos tipos de usuarios y evitamos la modificacion directa de esta clase.
    /// </summary>
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }

        /// <summary>
        /// Referencia a la gestión del sistema
        /// </summary>
        /// <remarks>
        /// **SOLID: Dependency Inversion Principle (DIP):** Depende de la ABSTRACCIÓN (`IGestionSistema`) y no de una clase concreta.
        /// **GRASP: Low Coupling:** El acoplamiento es bajo porque depende de una interfaz y no de una implementación concreta.
        /// </remarks>
        public IGestionSistema GestionSistema;
        public bool EstaSuspendido { get; set; }

        public Usuario(string esteNombre, string esteCorreo, string esteTelefono, IGestionUsuario gestionUsuario)
        {
            this.Nombre = esteNombre;
            this.Correo = esteCorreo;
            this.Telefono = esteTelefono;
            this.GestionSistema = estaGestionSistema;
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