using Library.interfaces;
using System;

namespace Library
{
    public class Vendedor: Usuario
    {

        public Vendedor(string esteNombre, string esteCorreo, string esteTelefono, IGestionUsuario estaGestionUsuario,  IGestionCliente estaGestionCliente) : base(esteNombre, esteCorreo,
            esteTelefono, estaGestionUsuario,estaGestionCliente)
        {

        }
        
        public void AsignarOtroVendedor(Vendedor vendedor, Cliente cliente)
        {
            if (this.GestionCliente.Clientes.Contains(cliente))
            {
                this.GestionCliente.EliminarCliente(cliente);
                vendedor.GestionCliente.AgregarCliente(cliente);    
            }
        
        }
    }
}
