using Discord;
using Discord.Interactions;
using Library;
using System.Threading.Tasks;

namespace Ucu.Poo.DiscordBot.Commands
{
    public class CommandCrearCliente : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly Fachada _fachada;

        public CommandCrearCliente(Fachada fachada)
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
    }
}