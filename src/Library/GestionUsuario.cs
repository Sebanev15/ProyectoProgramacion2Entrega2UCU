using System.Collections.Generic;
using Library.interfaces;
using NotImplementedException = System.NotImplementedException;

namespace Library
{
    public class GestionUsuario: IGestionUsuario
    {
        public List<Usuario> Usuarios { get; set; }
        public GestionUsuario()
        {
            this.Usuarios = new List<Usuario>();
        }
        
        public void CrearUsuario(Administrador administrador, Usuario usuario)
        {
            administrador.RegistrarUsuario(usuario,this);
        }        
        
        public List<Usuario> BuscarUsuario(string usuarioBusqueda)
        {
            List<Usuario> resultados = new List<Usuario>();
            
            foreach (Usuario usuario in Usuarios)
            {
                foreach (var informacionAtributo in usuario.GetType().GetProperties())
                {
                    var valorAtributo = informacionAtributo.GetValue(usuario);
                    if (valorAtributo is string)
                    {
                      
                        if (valorAtributo.Equals(usuarioBusqueda) && !resultados.Contains(usuario))
                        {
                            resultados.Add(usuario);
                        }
                      
                    }
                }
            }
            return resultados;
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