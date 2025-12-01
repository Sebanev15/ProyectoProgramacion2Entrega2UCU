using System.Collections.Generic;
using Library.interfaces;
using NotImplementedException = System.NotImplementedException;

namespace Library
{
    /// <summary>
    /// Clase que representa la gestion de los clientes, realiza las operaciones relacionada a
    /// los clientes y a las clases fuertemente asociadas.
    /// </summary>
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
        
        public List<Usuario> BuscarUsuario(List<string> datosBusqueda)
        {
            int cantidadDatos = datosBusqueda.Count;
            List<Usuario> resultados = new List<Usuario>();
            foreach (Usuario usuario in Usuarios)
            {
                int contador = 0;
                foreach (var informacionAtributo in usuario.GetType().GetProperties())
                {
                    var valorAtributo = informacionAtributo.GetValue(usuario);
                    if (valorAtributo is string)
                    {
                        foreach (string datosBuscados in datosBusqueda)
                        {
                            if (valorAtributo.Equals(datosBuscados) && !resultados.Contains(usuario) )
                            {
                                contador++;
                            }
                        }
                    }
                }
                if (cantidadDatos==contador)
                {
                    resultados.Add(usuario);
                }
            }
            return resultados;
        }
        
    }
}