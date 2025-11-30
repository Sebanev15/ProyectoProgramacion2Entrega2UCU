namespace Ucu.Poo.DiscordBot.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Ucu.Poo.DiscordBot.Domain;

public class CommandEliminarCliente: ModuleBase<SocketCommandContext>
{
    private readonly Fachada _fachada;

    CommandEliminarCliente(Fachada fachada)
    {
        _fachada = fachada;
    }

    [Command("eliminarCliente")]
    public async Task ExecuteAsync(Cliente cliente)
    {
        try
        {
            _fachada.EliminarCliente(cliente);
            await ReplyAsync($"Se eliminó el cliente {cliente.Nombre} del sistema");
        }
        catch (CampoInvalidoExepcion e)
        {
            await ReplyAsync(e.Message);
        }
    }
}