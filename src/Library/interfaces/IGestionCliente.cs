using System.Collections.Generic;
using System;

namespace Library.interfaces
{
    /// <summary>
    /// Esta interfaz define todas las operaciones relacionadas con la gestión de clientes,
    /// </summary>
    public interface IGestionCliente
    {
        /// <summary>
        /// Lista de interacciones registradas.
        /// </summary>
        List<IInteraccion> Interacciones { get; set; }
        /// <summary>
        /// Lista de importes registrados.
        /// </summary>
        List<IImporte>Importes { get; set; }
        /// <summary>
        /// Lista de clientes registrados.
        /// </summary>
        List<Cliente> Clientes { get; set; }

        /// <summary>
        /// Agrega una nueva interaccion a un cliente.
        /// </summary>
        /// <param name="cliente">Cliente a añadir interaccion</param>
        /// <param name="interaccion">Interaccion a añadir</param>
        void RegistrarInteraccion(Cliente cliente, IInteraccion interaccion);

        /// <summary>
        /// Busca interacciones de un cliente en una fecha especifica que contengan una cadena de busqueda.
        /// </summary>
        /// <param name="fecha">Fecha de la interaccion a buscar</param>
        /// <param name="busqueda">Cadena de filtro de interacciones</param>
        /// <param name="cliente">Cliente del cual buscar las interacciones</param>
        /// <returns>Retorna la lista de interacciones que cumplen con todas las condiciones</returns>
        List<IInteraccion> BuscarInteracciones(DateTime fecha, string busqueda, Cliente cliente);

        /// <summary>
        /// Agrega un comentario a una interaccion especifica.
        /// </summary>
        /// <param name="interaccion">Interaccion a la cual añadir el comentario</param>
        /// <param name="comentario">Comentario a añadir</param>
        void AgregarComentarioInteraccion(IInteraccion interaccion, string comentario);

        /// <summary>
        /// Obtiene las ventas totales de todos los clientes entre dos fechas.
        /// </summary>
        /// <param name="fechaInicio">Fecha de donde empieza a buscar datos</param>
        /// <param name="fechaFin">Fecha de donde finaliza de buscar datos</param>
        /// <returns></returns>
        List<String> ObtenerVentasTotales(DateTime fechaInicio, DateTime fechaFin);

        void AgregarImporte(IImporte importe, Cliente cliente);

        void AgregarCliente(Cliente cliente);
        void ModificarCliente(Cliente clienteBase, Cliente clienteModificado);

        void EliminarCliente(Cliente cliente);

        List<Cliente> BuscarCliente(string clienteBusqueda);

        void ListarClientes();

        void AgregarEtiqueta(Cliente cliente, Etiqueta etiqueta);
        
        List<Cliente> ObtenerClientesInactivos();

        List<Cliente> ObtenerClientesNoRespondidos();
        void ModificarImporte(IImporte importeBase, IImporte importeModificado);
        
        void EliminarImporte(IImporte importe);
    }
}
