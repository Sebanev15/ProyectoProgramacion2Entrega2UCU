using Library.abstractions;
using System;
namespace Library
{
    public class Administrador: UsuarioBase
    {
        public Administrador(string esteNombre, string esteCorreo, string esteTelefono) : base(esteNombre, esteCorreo,
            esteTelefono)
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
