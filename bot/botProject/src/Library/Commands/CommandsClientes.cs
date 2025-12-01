using Discord;
using Discord.Interactions;
using Library;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

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
                .WithCustomId("modal_crear_cliente")
                .AddTextInput("Nombre completo", "nombre_completo", placeholder: "Juan Pérez", required: true)
                .AddTextInput("Teléfono", "telefono", placeholder: "099123456", required: true)
                .AddTextInput("Correo", "correo", placeholder: "correo@example.com", required: true)
                .AddTextInput("Género (M/H)", "genero", placeholder: "M o H", maxLength: 1, required: true)
                .AddTextInput("Fecha nacimiento (YYYY-MM-DD)", "fecha_nac", placeholder: "2000-05-01", required: true);

            await RespondWithModalAsync(modal.Build());
        }

        [SlashCommand("mostrarclientes", "Muestra los clientes creados en formato de tabla")]
        public async Task MostrarClientesAsync()
        {
            var clientes = _fachada.ListarClientesConReturn();
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
    }
}
