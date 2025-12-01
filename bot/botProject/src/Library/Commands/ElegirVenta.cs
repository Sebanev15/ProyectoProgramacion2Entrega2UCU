using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Interactions;
using Library;
using Library.interfaces;

namespace Ucu.Poo.DiscordBot.Commands
{
    public class CommandElegirVenta : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("elegirventa", "Elige una venta de una lista de ventas repetidas")]
        public async Task ElegirVentaAsync(int numero)
        {
            List<Venta> lista;
            if (!SeleccionesUsuarios.OpcionesVenta.TryGetValue(Context.User.Id, out lista))
            {
                await ReplyAsync("No tenés ninguna selección activa.");
                return;
            }

            if (numero < 1 || numero > lista.Count)
            {
                await ReplyAsync("Número inválido. Elegí un número de la lista mostrada.");
                return;
            }

            Venta venta = lista[numero - 1];

            await ReplyAsync(
                $" Seleccionaste: {venta.Producto}.\n" +
                $"Ahora podés volver a ejecutar usando exactamente ese nombre.");
        }
    }
}