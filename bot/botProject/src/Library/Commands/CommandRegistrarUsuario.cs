namespace Ucu.Poo.DiscordBot.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Ucu.Poo.DiscordBot.Domain;

public class CommandReistrarUsuario : ModuleBase<SocketCommandContext>
{
    private readonly Fachada _fachada;

    CommandReistrarUsuario(Fachada fachada)
    {
        _fachada = fachada;
    }
    [Command("registrarUsuario")]
    public async Task ExecuteAsync(Administrador administrador, Usuario usuario)
    {
        try
        {
            _fachada.RegistrarUsuario(administrador, usuario);
            await ReplyAsync($"Se registro al usuario {usuario.Nombre}");
        }
        catch (CampoInvalidoExepcion e)
        {
            await ReplyAsync(e.Message);
        }
    }

}