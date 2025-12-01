using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Interactions;
using Library;
using Library.interfaces;

namespace Ucu.Poo.DiscordBot.Commands
{
    public class CommandElegirCotizacion : InteractionModuleBase<SocketInteractionContext>
    {
        [SlashCommand("elegircotizacion", "Elige una cotizacion de una lista de cotizaciones repetidas")]
        public async Task ElegirCotizacionAsync(int numero)
        {
            List<Cotizacion> lista;
            if (!SeleccionesUsuarios.OpcionesCotizacion.TryGetValue(Context.User.Id, out lista))
            {
                await ReplyAsync("No tenés ninguna selección activa.");
                return;
            }

            if (numero < 1 || numero > lista.Count)
            {
                await ReplyAsync("Número inválido. Elegí un número de la lista mostrada.");
                return;
            }

            Cotizacion cotizacion = lista[numero - 1];

            await ReplyAsync(
                $" Seleccionaste la cotizacion número: {numero}.\n" +
                $"Ahora podés volver a ejecutar usando exactamente ese nombre.");
            
            SeleccionesUsuarios.OpcionesCotizacion.Remove(Context.User.Id);
        }
    }
}