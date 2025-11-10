using System.Collections.Generic;
using Library.interfaces;
using NotImplementedException = System.NotImplementedException;

namespace Library
{
    public class GestionUsuario: IGestionUsuario
    {
        public List<Usuario> Usuarios { get; set; }
        
        public void CrearUsuario(Administrador administrador, Usuario usuario)
        {
            administrador.CrearUsuario(usuario,this);
        }

        public void SuspenderUsuario(Administrador administrador, Usuario usuario)
        {
            administrador.SuspenderUsuario(usuario);
        }

        public void ReactivarUsuario(Administrador administrador, Usuario usuario)
        {
            administrador.ReactivarUsuario(usuario);
        }

        public void EliminarUsuario(Administrador administrador, Usuario usuario)
        {
            administrador.EliminarUsuario(usuario,this);
        }

        public void AsignarOtroVendedor(Vendedor vendedor1,Vendedor vendedor2, Cliente cliente)
        {
            vendedor1.AsignarOtroVendedor(vendedor2,cliente);
        }
    }
}