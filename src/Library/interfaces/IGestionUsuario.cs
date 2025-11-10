using System.Collections.Generic;

namespace Library.interfaces
{
    public interface IGestionUsuario
    {
        List<Usuario> Usuarios { get; set; }

        void CrearUsuario(Administrador administrador, Usuario usuario);

        void SuspenderUsuario(Administrador administrador, Usuario usuario);

        void ReactivarUsuario(Administrador administrador, Usuario usuario);

        void EliminarUsuario(Administrador administrador, Usuario usuario);

        void AsignarOtroVendedor(Vendedor vendedor1,Vendedor vendedor2, Cliente cliente);
    }
}