using Discord;
using Discord.Interactions;
using Library;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Library.interfaces;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands
{
    public class CommandsInteraccion : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly Fachada _fachada;

        public CommandsInteraccion(Fachada fachada)
        {
            _fachada = fachada;
        }

        [SlashCommand("crearInteraccion", "Crear una Interaccion, seleccione a traves del select el tipo de interaccion")]
        public async Task AbrirModalAsync()
        {
            var modal = new ModalBuilder()
                .WithTitle("Crear nuevo Mensaje")
                .WithCustomId("modal_global:crear_Mensaje")
                .AddTextInput("Fecha", "nombre_completo", placeholder: "2000-05-01", required: true)
                .AddTextInput("Tema", "tema", placeholder: "reunion a las 4am", required: true)
                .AddTextInput("Comentarios", "comentario", placeholder: "muy tarde", required: true)
                .AddTextInput("Cliente", "cliente", placeholder: "pablo", required: true)
                .AddTextInput("Usuario", "genero", placeholder: "jorge", maxLength: 1, required: true)
                .AddTextInput("EsEnviado", "enviado", placeholder: "Si/No", required: true);

            await RespondWithModalAsync(modal.Build());
        }

        [SlashCommand("modificarcliente", "Modificar un cliente existente con formulario")]
        public async Task ModificarModalAsync(string parametrosDeBusqueda)
        {
            List<string> parametros = new List<string> {};
            foreach (string parametro in parametrosDeBusqueda.Split(' '))
            {
                parametros.Add(parametro);
            }
            List<Cliente> clientea = _fachada.BuscarCliente(parametros);
            if (clientea.Count != 1)
            {
                await RespondAsync($"No se encontró un cliente único con los parametros {parametrosDeBusqueda}", ephemeral: true);
                return;
            }
            Cliente clienteEncontrado = clientea[0];

            var modal = new ModalBuilder()
                .WithTitle("Modificar cliente")
                .WithCustomId($"modal_global:modificar_cliente")
                .AddTextInput("Nombre completo", "nombre_completo",
                    value: (clienteEncontrado.Nombre + " " + clienteEncontrado.Apellido), required: true)
                .AddTextInput("Género (M/H)", "genero", value: clienteEncontrado.Genero, placeholder: "M o H",
                    maxLength: 1, required: false)
                .AddTextInput("Teléfono", "telefono", value: clienteEncontrado.Telefono, placeholder: "099123456", required: false)
                .AddTextInput("Correo", "correo", value: clienteEncontrado.Correo, placeholder: "correo@example.com", required: false)
                .AddTextInput("Fecha nacimiento (YYYY-MM-DD)", "fecha_nac", value: clienteEncontrado.FechaDeNacimiento.ToString("yyyy-MM-dd"), required: false);

            await RespondWithModalAsync(modal.Build());
        }
        
        [SlashCommand("eliminarcliente", "Elimina un cliente existente")]
        public async Task EliminarClienteAsync(string parametrosDeBusqueda)
        {
            List<string> parametros = new List<string> {};
            foreach (string parametro in parametrosDeBusqueda.Split(' '))
            {
                parametros.Add(parametro);
            }
            List<Cliente> clientes = _fachada.BuscarCliente(parametros);
            if (clientes.Count != 1)
            {
                await RespondAsync($"No se encontró un cliente único con los parametros {parametrosDeBusqueda}.", ephemeral: true);
                return;
            }
            Cliente clienteEncontrado = clientes[0];
            _fachada.EliminarCliente(clienteEncontrado);
            await RespondAsync($"Cliente {clienteEncontrado.Nombre} {clienteEncontrado.Apellido} eliminado correctamente.");
        }

        [SlashCommand("buscarclientes", "Busca clientes por parámetros")]
        public async Task BuscarClientesAsync(string parametrosDeBusqueda)
        {
            List<string> parametros = new List<string> {};
            foreach (string parametro in parametrosDeBusqueda.Split(' '))
            {
                parametros.Add(parametro);
            }
            List<Cliente> clientes = _fachada.BuscarCliente(parametros);
            if (clientes.Count == 0)
            {
                await RespondAsync($"No se encontró ningún cliente con los parametros {parametrosDeBusqueda}.");
                return;
            }

            var listaClientes = new StringBuilder();
            int i = 1;
            foreach (var cliente in clientes)
            {
                listaClientes.AppendLine($"{i}. {cliente.Nombre} {cliente.Apellido} - Tel: {cliente.Telefono} - Correo: {cliente.Correo} - Género: {cliente.Genero} - Fecha Nac: {cliente.FechaDeNacimiento:yyyy-MM-dd}");
                i++;
            }

            await RespondAsync(listaClientes.ToString());
        }

        [SlashCommand("mostrarclientes", "Muestra los clientes creados en formato de tabla")]
        public async Task MostrarClientesAsync()
        {
            List<Cliente> clientes = _fachada.ListarClientesConReturn();
            if (clientes.Count == 0)
            {
                await RespondAsync("No hay clientes registrados.");
                return;
            }

            var listaClientes = new StringBuilder();
            int i = 1;
            foreach (var cliente in clientes)
            {
                listaClientes.AppendLine($"{i}. {cliente.Nombre} {cliente.Apellido} - Tel: {cliente.Telefono} - Correo: {cliente.Correo} - Género: {cliente.Genero} - Fecha Nac: {cliente.FechaDeNacimiento:yyyy-MM-dd}");
                i++;
            }

            await RespondAsync(listaClientes.ToString());
        }

        [SlashCommand("clientesinactivos", "Muestra los clientes que no han tenido interacciones recientes")]
        public async Task ClientesInactivosAsync()
        {
            List<Cliente> clientesInactivos;
            try
            {
                clientesInactivos = _fachada.ObtenerClientesInactivos();
            }
            catch (ListaVaciaExcepcion)
            {
                await RespondAsync("No hay clientes inactivos.");
                return;
            }

            var listaClientes = new StringBuilder();
            int i = 1;
            foreach (var cliente in clientesInactivos)
            {
                listaClientes.AppendLine($"{i}. {cliente.Nombre} {cliente.Apellido} - Tel: {cliente.Telefono} - Correo: {cliente.Correo} - Género: {cliente.Genero} - Fecha Nac: {cliente.FechaDeNacimiento:yyyy-MM-dd}");
                i++;
            }

            await RespondAsync(listaClientes.ToString());
        }

        [SlashCommand("buscarinteraccion", "Busca interacciones por parámetros")]
        public async Task BuscarInteraccionAsync(string parametrosDeBusqueda, string clienteBusqueda)
        {
            List<string> parametros = new List<string> {};
            foreach (string parametro in parametrosDeBusqueda.Split(' '))
            {
                parametros.Add(parametro);
            }

            foreach (Cliente cliente in _fachada.GetGestionCliente().BuscarCliente(parametros))
            {
                List<IInteraccion> interacciones = _fachada.BuscarInteraccionesSinFecha(parametros, cliente);
                if (interacciones.Count == 0)
                {
                    await RespondAsync($"No se encontró ninguna interaccion con los parametros {parametrosDeBusqueda}.");
                    return;
                }

                var listaInteracciones = new StringBuilder();
                int i = 1;
                foreach (var interaccion in interacciones)
                {
                    listaInteracciones.AppendLine($"{i}. Fecha: {interaccion.Fecha.ToString()} - Tema: {interaccion.Tema} - Cliente: {interaccion.Cliente} - Usuario: {interaccion.Usuario} - Comentarios: {interaccion.Comentarios.ToString()}");
                    i++;
                }

                await RespondAsync(listaInteracciones.ToString());
            }
        }
        
    }
}