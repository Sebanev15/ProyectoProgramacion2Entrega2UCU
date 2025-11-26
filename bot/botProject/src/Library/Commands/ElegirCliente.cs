using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using Library;

namespace Ucu.Poo.DiscordBot.Commands
{
    public class CommandElegirCliente : ModuleBase<SocketCommandContext>
    {
        [Command("elegirCliente")]
        public async Task ElegirClienteAsync(int numero)
        {
            List<Cliente> lista;
            if (!SeleccionesUsuarios.OpcionesClientes.TryGetValue(Context.User.Id, out lista))
            {
                await ReplyAsync("No tenés ninguna selección activa.");
                return;
            }

            if (numero < 1 || numero > lista.Count)
            {
                await ReplyAsync("Número inválido. Elegí un número de la lista mostrada.");
                return;
            }

            Cliente cliente = lista[numero - 1];

            await ReplyAsync(
                $" Seleccionaste: {cliente.Nombre}.\n" +
                $"Ahora podés volver a ejecutar usando exactamente ese nombre.");
            
            SeleccionesUsuarios.OpcionesClientes.Remove(Context.User.Id);
        }
    }
}