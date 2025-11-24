using System.Threading.Tasks;
using Discord.Commands;

namespace Ucu.Poo.DiscordBot.Commands
{
    public class CommandElegirCliente : ModuleBase<SocketCommandContext>
    {
        [Command("elegirCliente")]
        public async Task ElegirClienteAsync(int numero)
        {
            if (!SeleccionesUsuarios.OpcionesClientes.TryGetValue(Context.User.Id, out var lista))
            {
                await ReplyAsync("No tenés ninguna selección activa. Usá `!crearCotizacion` primero.");
                return;
            }

            if (numero < 1 || numero > lista.Count)
            {
                await ReplyAsync("Número inválido. Elegí un número de la lista mostrada.");
                return;
            }

            var cliente = lista[numero - 1];

            await ReplyAsync(
                $"✅ Seleccionaste: **{cliente.Nombre}**.\n" +
                $"Ahora podés volver a ejecutar `!crearCotizacion` usando exactamente ese nombre.");
            
            SeleccionesUsuarios.OpcionesClientes.Remove(Context.User.Id);
        }
    }
}