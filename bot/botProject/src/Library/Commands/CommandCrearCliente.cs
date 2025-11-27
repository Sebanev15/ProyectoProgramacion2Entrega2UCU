using Discord;
using Discord.Interactions;
using Library;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

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

        [SlashCommand("mostrarclientes", "Muestra los clientes creados en formato de tabla")]
        public async Task MostrarClientesAsync()
        {
            await DeferAsync();

            List<Cliente> clientes = _fachada.ListarClientesConReturn();
            if (clientes.Count == 0)
            {
                await FollowupAsync("No hay clientes creados.");
                return;
            }

            // Encabezado de la tabla
            string header = "```md\n# Lista de Clientes\n" +
                            "---------------------------------------------------------------------------------\n" +
                            $"| {"Nombre Completo",-20} | {"Teléfono",-12} | {"Correo",-25} | {"Nacimiento",-12} |\n" +
                            "---------------------------------------------------------------------------------\n";
            string footer = "---------------------------------------------------------------------------------\n```";

            var tabla = new StringBuilder(header);
            bool firstMessageSent = false;

            foreach (var cliente in clientes)
            {
                string nombreCompleto = $"{cliente.Nombre} {cliente.Apellido}";
                nombreCompleto = nombreCompleto.Length > 20 ? nombreCompleto.Substring(0, 17) + "..." : nombreCompleto;
                string correo = cliente.Correo.Length > 25 ? cliente.Correo.Substring(0, 22) + "..." : cliente.Correo;
                string fila = $"| {nombreCompleto,-20} | {cliente.Telefono,-12} | {correo,-25} | {cliente.FechaDeNacimiento.ToShortDateString(),-12} |\n";
                
                if (tabla.Length + fila.Length + footer.Length > 2000)
                {
                    tabla.AppendLine(footer);
                    if (!firstMessageSent)
                    {
                        await FollowupAsync(tabla.ToString());
                        firstMessageSent = true;
                    }
                    else
                    {
                        await ReplyAsync(tabla.ToString());
                    }
                    
                    tabla.Clear();
                    tabla.Append(header);
                }
                tabla.Append(fila);
            }

            tabla.AppendLine(footer);
            if (!firstMessageSent)
            {
                await FollowupAsync(tabla.ToString());
            }
            else
            {
                await ReplyAsync(tabla.ToString());
            }
        }
    }
}
