using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Interactions;
using Library;

namespace Ucu.Poo.DiscordBot.Commands
{
    public class CommandElegirCliente : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("elegircliente", "Elige un cliente de una lista de nombres repetidos")]
        public async Task ElegirClienteAsync(int numero)
        {
            List<Cliente> lista;
            if (!Selecciones.OpcionesClientes.TryGetValue(Context.User.Id, out lista))
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
            
            Selecciones.ClienteSeleccionado[Context.User.Id] = cliente;
            
            await ReplyAsync(
                $" Seleccionaste: {cliente.Nombre}.\n" +
                $"Ahora podés volver a ejecutar usando exactamente ese nombre.");
        }
    }
}