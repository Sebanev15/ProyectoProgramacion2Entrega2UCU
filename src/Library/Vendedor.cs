using Library.abstractions;
using Library.interfaces;
using System;

namespace Library
{
    public class Vendedor: UsuarioBase
    {
        public Vendedor(string esteNombre, string esteCorreo, string esteTelefono) : base(esteNombre, esteCorreo,
            esteTelefono)
        {

        }

        public void AsignarOtroVendedor(Vendedor vendedor, IClienteBase cliente)
        {
            if (this.GestionSistema.GestionCliente.Clientes.Contains(cliente))
            {
                this.GestionSistema.GestionCliente.EliminarCliente(cliente);
                vendedor.GestionSistema.GestionCliente.AgregarCliente(cliente);    
            }
            else
            {
                Console.WriteLine("ERROR: El cliente no existe");
            }
        
        }
    }
}
