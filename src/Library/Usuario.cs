using Library.interfaces;

namespace Library
{

    /// <summary>
    /// Clase base para los usuarios del Cliente.
    /// haciendo esta clase abstracta, se cumple con OCP (Open/Closed Principle) ya que permitimos extender a nuevos tipos de usuarios y evitamos la modificacion directa de esta clase.
    /// </summary>
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        
        /// <summary>
        /// Referencia a la gestión del Cliente
        /// </summary>
        /// <remarks>
        /// **SOLID: Dependency Inversion Principle (DIP):** Depende de la ABSTRACCIÓN (`IGestionCliente`) y no de una clase concreta.
        /// **GRASP: Low Coupling:** El acoplamiento es bajo porque depende de una interfaz y no de una implementación concreta.
        /// </remarks>
        
        public IGestionUsuario GestionUsuario;

        public IGestionCliente GestionCliente;
        public bool EstaSuspendido { get; set; }

        public Usuario(string esteNombre, string esteCorreo, string esteTelefono, IGestionUsuario estaGestionUsuario )
        {
            this.Nombre = esteNombre;
            this.Correo = esteCorreo;
            this.Telefono = esteTelefono;
            this.GestionUsuario = estaGestionUsuario;
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
