using System;
using Library.interfaces;

namespace Library
{
    /// <summary>
    /// Esta clase representa a un Usuario de tipo Administrador, este tipo de Usuario tiene permisos para realizar operaciones sobre otras clases.
    /// </summary>
    public class Administrador: Usuario
    {
        /// <summary>
        /// Constructor de la clase, Se pide el nombre, correo, telefono, gestionUsuario, GestionCliente
        /// </summary>
        /// <param name="esteNombre"></param> Nombre del Usuario Administrador.
        /// <param name="esteCorreo"></param> Correo del Usuario Administrador.
        /// <param name="esteTelefono"></param>Telefono del Usuario Administrador.
        /// <param name="estaGestionUsuario"></param> Atributo GestionUsuario perteneciente al Usuario Administrador,
        /// se usa para realizar operaciones especiales sobre los Usuarios.
        /// <param name="estaGestionCliente"></param> Atributo GestionUsuario perteneciente al Usuario Administrador,
        /// se usa para realizar operaciones especiales sobre los Clientes.
        public Administrador(string esteNombre, string esteCorreo, string esteTelefono, IGestionUsuario estaGestionUsuario, IGestionCliente estaGestionCliente) : base(esteNombre, esteCorreo, 
            esteTelefono, estaGestionUsuario, estaGestionCliente)
        {

        }
    /// <summary>
    /// Añade un usuario a la lista de usuarios que contiene gestionUsuario.
    /// </summary>
    /// <param name="usuario"></param> Usuario a agregar
    /// <param name="gestionUsuario"></param> gestionUsuario donde se guarda
        public void CrearUsuario(Usuario usuario,IGestionUsuario gestionUsuario)
        {
            if (!gestionUsuario.Usuarios.Contains(usuario))
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
     /// <summary>
     /// Al usuario dado se le reactivara en caso que EstadoSuspendido=false.
     /// </summary>
     /// <param name="usuario"></param> Usuario a reactivar.
        public void ReactivarUsuario(Usuario usuario)
        {
            usuario.Reactivar();
        }
        /// <summary>
        /// Quita a un usuario de la lista de usuarios en gestionUsuario.
        /// </summary>
        /// <param name="usuario"></param> Usuario a eliminar
        /// <param name="gestionUsuario"></param> Lista de donde se elimina en gestionUsuario
        public void EliminarUsuario(Usuario usuario, IGestionUsuario gestionUsuario)
        {
            gestionUsuario.Usuarios.Remove(usuario);
        }
    }
}
