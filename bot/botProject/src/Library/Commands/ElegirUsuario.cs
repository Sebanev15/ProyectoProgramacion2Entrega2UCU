using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Interactions;
using Library;

namespace Ucu.Poo.DiscordBot.Commands
{
    public class CommandElegirUsuario : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("elegirusuario", "Elige un usuario de un lista de nombres repetidos")]
        public async Task ElegirUsuarioAsync(int numero)
        {
            List<Usuario> lista;
            if (!Selecciones.OpcionesUsuarios.TryGetValue(Context.User.Id, out lista))
            {
                await ReplyAsync("No tenés ninguna selección activa.");
                return;
            }

            if (numero < 1 || numero > lista.Count)
            {
                await ReplyAsync("Número inválido. Elegí un número de la lista mostrada.");
                return;
            }

            Usuario usuario = lista[numero - 1];

            Selecciones.UsuarioSeleccionado[Context.User.Id] = usuario;
            
            await ReplyAsync(
                $" Seleccionaste: {usuario.Nombre}.\n" +
                $"Ahora podés volver a ejecutar usando exactamente ese nombre.");
        }
    }
}