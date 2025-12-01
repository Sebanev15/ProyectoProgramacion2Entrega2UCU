using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Interactions;
using Library;

namespace Ucu.Poo.DiscordBot.Commands;

public class ElegirVendedorInicial: InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("elegirvendedorinicial","Elige un Vendedor inicial de una lista de nombres repetidos")]
    public async Task ElegirVendedorInicialAsync(int numero)
    {
        if (!Selecciones.OpcionesVendedorInicial.TryGetValue(Context.User.Id, out List<Usuario> lista))
        {
            await ReplyAsync("No tenés ninguna selección activa de vendedores iniciales.");
            return;
        }

        if (numero < 1 || numero > lista.Count)
        {
            await ReplyAsync("Número inválido. Elegí un número válido de la lista.");
            return;
        }

        Usuario vendedor = lista[numero - 1];

        Selecciones.VendedorInicialSeleccionado[Context.User.Id] = vendedor;

        await ReplyAsync($"Vendedor inicial seleccionado: {vendedor.Nombre}");

        Selecciones.OpcionesVendedorInicial.Remove(Context.User.Id);
    }

}