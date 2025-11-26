namespace Ucu.Poo.DiscordBot.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Ucu.Poo.DiscordBot.Domain;

public class CommandCrearEtiqueta : ModuleBase<SocketCommandContext>
{
    private readonly Fachada _fachada;

    CommandCrearEtiqueta(Fachada fachada)
    {
        _fachada = fachada;
    }
    [Command("crearEtiqueta")]
    public async Task ExecuteAsync(string nombre)
    {
        try
        {
            _fachada.CrearEtiqueta(nombre);
            await ReplyAsync($"Se creó la etiqueta **{nombre}**");
        }
        catch (CampoInvalidoExepcion e)
        {
            await ReplyAsync(e.Message);
        }
    }

}