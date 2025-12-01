using Discord;
using Discord.Interactions;
using Library;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands
{
    public class CommandsClientes : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly Fachada _fachada;

        public CommandsClientes(Fachada fachada)
        {
            _fachada = fachada;
        }

        [SlashCommand("crearcliente", "Crear un cliente con formulario")]
        public async Task AbrirModalAsync()
        {
            var modal = new ModalBuilder()
                .WithTitle("Crear nuevo cliente")
                .WithCustomId("modal_global:crear_cliente")
                .AddTextInput("Nombre completo", "nombre_completo", placeholder: "Juan Pérez", required: true)
                .AddTextInput("Teléfono", "telefono", placeholder: "099123456", required: true)
                .AddTextInput("Correo", "correo", placeholder: "correo@example.com", required: true)
                .AddTextInput("Género (M/H)", "genero", placeholder: "M o H", maxLength: 1, required: true)
                .AddTextInput("Fecha nacimiento (YYYY-MM-DD)", "fecha_nac", placeholder: "2000-05-01", required: true);

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

        [SlashCommand("clientesnorespondidos", "Muestra los clientes que no han respondido a interacciones")]
        public async Task ClientesNoRespondidosAsync()
        {
            List<Cliente> clientesNoRespondidos;
            try
            {
                clientesNoRespondidos = _fachada.ObtenerClientesNoRespondidos();
            }
            catch (ListaVaciaExcepcion)
            {
                await RespondAsync("No hay clientes no respondidos.");
                return;
            }
            var listaClientes = new StringBuilder();
            int i = 1;
            foreach (var cliente in clientesNoRespondidos)
            {
                listaClientes.AppendLine($"{i}. {cliente.Nombre} {cliente.Apellido} - Tel: {cliente.Telefono} - Correo: {cliente.Correo} - Género: {cliente.Genero} - Fecha Nac: {cliente.FechaDeNacimiento:yyyy-MM-dd}");
                i++;
            }
            await RespondAsync(listaClientes.ToString());
            
        }
        
    }
}
