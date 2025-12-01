/*
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Ucu.Poo.DiscordBot.Domain;

public class CommandReactivarUsuario: ModuleBase<SocketCommandContext>
{
    private readonly Fachada _fachada;

    CommandReactivarUsuario(Fachada fachada)
    {
        _fachada = fachada;
    }

    [Command("reactivarUsuario")]
    public async Task ExecuteAsync(Administrador admin, Usuario usuario)
    {
        try
        {
            _fachada.ReactivarUsuario(admin,usuario);
            await ReplyAsync($"Se reactivo el usuario {usuario.Nombre}");
        }
        catch (CampoInvalidoExepcion e)
        {
            await ReplyAsync(e.Message);
        }
    }
}
*/