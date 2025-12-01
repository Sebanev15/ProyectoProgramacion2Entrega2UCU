using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Interactions;
using Library;

namespace Ucu.Poo.DiscordBot.Commands;

public class ElegirVendedorAsignado: InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("elegirvendedorasignado","Elige un Vendedor inicial de una lista de nombres repetidos")]
    public async Task ElegirVendedorAsignadoAsync(int numero)
    {
        if (!Selecciones.OpcionesVendedorAsignado.TryGetValue(Context.User.Id, out List<Usuario> lista))
        {
            await ReplyAsync("No tenés ninguna selección activa de vendedores asignados.");
            return;
        }

        if (numero < 1 || numero > lista.Count)
        {
            await ReplyAsync("Número inválido. Elegí un número válido de la lista.");
            return;
        }

        Usuario vendedor = lista[numero - 1];

        Selecciones.VendedorAsignadoSeleccionado[Context.User.Id] = vendedor;

        await ReplyAsync($"Vendedor asignado seleccionado: {vendedor.Nombre}");

        Selecciones.OpcionesVendedorAsignado.Remove(Context.User.Id);
    }

}