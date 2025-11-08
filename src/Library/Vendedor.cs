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

        public void AsignarOtroVendedor(Vendedor vendedor, Cliente cliente)
        {
            if (this.GestionSistema.Clientes.Contains(cliente))
            {
                this.GestionSistema.EliminarCliente(cliente);
                vendedor.GestionSistema.AgregarCliente(cliente);    
            }
            else
            {
                Console.WriteLine("ERROR: El cliente no existe");
            }
        
        }
    }
}
