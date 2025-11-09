using System.Collections.Generic;
using System;

namespace Library.interfaces
{
    public interface IGestionSistema
    {
        List<IInteraccion> Interacciones { get; set; }
        List<IImporte>Importes { get; set; }
        List<Cliente> Clientes { get; set; }

        void RegistrarInteraccion(Cliente cliente, IInteraccion interaccion);

        List<IInteraccion> BuscarInteracciones(DateTime fecha, string busqueda);

        void AgregarComentario(IInteraccion interaccion, string comentario);

        List<IImporte> ObtenerVentasTotales(DateTime fechaInicio, DateTime fechaFin);

        void AgregarImporte(IImporte importe, Cliente cliente);

        void AgregarCliente(Cliente cliente);
        void ModificarCliente(Cliente clienteBase, Cliente clienteModificado);

        void EliminarCliente(Cliente cliente);

        List<Cliente> BuscarCliente(string clienteBusqueda);

        void ListarClientes();

        void AgregarEtiqueta(Cliente cliente, Etiqueta etiqueta);
        
        List<Cliente> ObtenerClientesInactivos();

        List<Cliente> ObtenerClientesNoRespondidos();
    }
}