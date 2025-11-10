using Library.interfaces;
using System;

namespace Library
{
    public class Vendedor: Usuario
    {
        public Vendedor(string esteNombre, string esteCorreo, string esteTelefono, IGestionCliente estaGestionCliente) : base(esteNombre, esteCorreo,
            esteTelefono, estaGestionCliente)
        {

        }
        
        public void AsignarOtroVendedor(Vendedor vendedor, Cliente cliente)
        {
            if (this.GestionCliente.Clientes.Contains(cliente))
            {
                this.GestionCliente.EliminarCliente(cliente);
                vendedor.GestionCliente.AgregarCliente(cliente);    
            }
            else
            {
                Console.WriteLine("ERROR: El cliente no existe");
            }
        
        }
    }
}
