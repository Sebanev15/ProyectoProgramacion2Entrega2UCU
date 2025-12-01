using Library.interfaces;
using System;
using Ucu.Poo.DiscordBot.Domain;

namespace Library
{
    /// <summary>
    /// Clase que represnta a un Usuario tipo Vendedor.
    /// </summary>
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
            else
            {
                throw new CampoInvalidoExepcion("El cliente no existe.");
            }
        
        }
    }
}
