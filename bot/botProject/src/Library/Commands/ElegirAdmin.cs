using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Interactions;
using Library;

namespace Ucu.Poo.DiscordBot.Commands;

public class ElegirAdmin: InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("elegiradmin","Elige un admin de un lista de duplicados")]
    public async Task ElegirAdminAsync(int numero)
    {
        if (!Selecciones.OpcionesAdmins.TryGetValue(Context.User.Id, out List<Administrador> lista))
        {
            await ReplyAsync("No tenés selección activa de admins.");
            return;
        }

        if (numero < 1 || numero > lista.Count)
        {
            await ReplyAsync("Número inválido.");
            return;
        }

        Usuario elegido = lista[numero - 1];

        if (elegido is not Administrador admin)
        {
            await ReplyAsync("El usuario elegido no es un administrador.");
            return;
        }

        Selecciones.AdminSeleccionado[Context.User.Id] = admin;

        await ReplyAsync($"Administrador seleccionado: {admin.Nombre}");
        
    }

}