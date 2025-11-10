using Library.abstractions;
using System;
using Library.interfaces;

namespace Library
{
    public class Administrador: Usuario
    {
        public Administrador(string esteNombre, string esteCorreo, string esteTelefono, IGestionSistema estaGestionSistema) : base(esteNombre, esteCorreo,
            esteTelefono, estaGestionSistema)
        {

        }
    
        public void CrearUsuario(Usuario usuario,GestionUsuario gestionUsuario)
        {
            if (gestionUsuario.Usuarios.Contains(usuario))
            {
                
            }
            else
            {
                gestionUsuario.Usuarios.Add(usuario);   
            }
        
        }

        /// <summary>
        /// Suspende las funciones de un usuario.
        /// </summary>
        /// <remarks> .
        /// **SOLID: Liskov Substitution Principle (LSP):** El método recibe un 'UsuarioBase', permitiendo que cualquier subclase pueda ser suspendida.
        /// **GRASP: Information Expert (Delegación):** El Administrador DELEGA la acción al objeto usuario, ya que el usuario es el experto en gestionar su propio estado.
        /// </remarks>
        public void SuspenderUsuario(Usuario usuario)
        {
            usuario.Suspender();
        }
    
        public void ReactivarUsuario(Usuario usuario)
        {
            usuario.Reactivar();
        }

        public void EliminarUsuario(Usuario usuario, GestionUsuario gestionUsuario)
        {
            gestionUsuario.Usuarios.Remove(usuario);
        }
    }
}
