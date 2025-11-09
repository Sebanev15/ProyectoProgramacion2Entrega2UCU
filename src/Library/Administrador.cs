using Library.abstractions;
using System;
using Library.interfaces;

namespace Library
{
    public class Administrador: UsuarioBase
    {
        public Administrador(string esteNombre, string esteCorreo, string esteTelefono, IGestionSistema estaGestionSistema) : base(esteNombre, esteCorreo,
            esteTelefono, estaGestionSistema)
        {

        }
    
        public void CrearUsuario(UsuarioBase usuario, System sistema)
        {
            if (sistema.usuarios.Contains(usuario))
            {
                Console.WriteLine("ERROR: No se pudo añadir el usuario al sistema");
            }
            else
            {
                sistema.usuarios.Add(usuario);   
            }
        
        }

        /// <summary>
        /// Suspende las funciones de un usuario.
        /// </summary>
        /// <remarks> .
        /// **SOLID: Liskov Substitution Principle (LSP):** El método recibe un 'UsuarioBase', permitiendo que cualquier subclase pueda ser suspendida.
        /// **GRASP: Information Expert (Delegación):** El Administrador DELEGA la acción al objeto usuario, ya que el usuario es el experto en gestionar su propio estado.
        /// </remarks>
        public void SuspenderUsuario(UsuarioBase usuario)
        {
            usuario.Suspender();
        }
    
        public void ReactivarUsuario(UsuarioBase usuario)
        {
            usuario.Reactivar();
        }

        public void EliminarUsuario(UsuarioBase usuario, System sistema)
        {
            if (sistema.usuarios.Contains(usuario))
            {
                sistema.usuarios.Remove(usuario);
            }
            else
            {
                Console.WriteLine("ERROR: No se pudo eliminar el usuario porque no estaba en el sistema");
            }
        }
    }
}
