/*
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Ucu.Poo.DiscordBot.Domain;

public class CommandSuspenderUsuario: ModuleBase<SocketCommandContext>
{
    private readonly Fachada _fachada;

    CommandSuspenderUsuario(Fachada fachada)
    {
        _fachada = fachada;
    }

    [Command("suspenderUsuario")]
    public async Task ExecuteAsync(Administrador admin, Usuario usuario)
    {
        try
        {
            _fachada.SuspenderUsuario(admin,usuario);
            await ReplyAsync($"Se suspendió al usuario {usuario.Nombre}");
        }
        catch (CampoInvalidoExepcion e)
        {
            await ReplyAsync(e.Message);
        }
    }
}
*/