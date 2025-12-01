using System;
using Library.interfaces;
using Ucu.Poo.DiscordBot.Domain;

namespace Library
{
    /// <summary>
    /// Clase que representa a un Usuario tipo Administrador.s
    /// </summary>
    public class Administrador: Usuario
    {
        public Administrador(string esteNombre, string esteCorreo, string esteTelefono, IGestionUsuario estaGestionUsuario, IGestionCliente estaGestionCliente) : base(esteNombre, esteCorreo, 
            esteTelefono, estaGestionUsuario, estaGestionCliente)
        {

        }
    
        public void RegistrarUsuario(Usuario usuario,IGestionUsuario gestionUsuario)
        {
            if (!gestionUsuario.Usuarios.Contains(usuario))
            {
                gestionUsuario.Usuarios.Add(usuario);   
            }
            else
            {
                throw new ItemDuplicadoExcepcion($"El usuario {usuario.Nombre} ya esta registrado");
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
            if (usuario != null)
            {
                throw new CampoInvalidoExepcion("Falta ingresar el usuario.");
            }
            usuario.Suspender();
        }
    
        public void ReactivarUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                throw new CampoInvalidoExepcion("Falta ingresar el usuario.");
            }
            usuario.Reactivar();
        }

        public void EliminarUsuario(Usuario usuario, IGestionUsuario gestionUsuario)
        {         
            if (usuario != null)
            {
                throw new CampoInvalidoExepcion("Falta ingresar el usuario.");
            }

            if (!gestionUsuario.Usuarios.Contains(usuario))
            {
                throw new CampoInvalidoExepcion("El usuario no esta registrado.");
            }
            gestionUsuario.Usuarios.Remove(usuario);
        }
    }
}
