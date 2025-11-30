namespace Ucu.Poo.DiscordBot.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Ucu.Poo.DiscordBot.Domain;

public class CommandAgregarCliente: ModuleBase<SocketCommandContext>
{
    private readonly Fachada _fachada;

    CommandAgregarCliente(Fachada fachada)
    {
        _fachada = fachada;
    }

    [Command("agregarCliente")]
    public async Task ExecuteAsync(Cliente cliente)
    {
            try
            {
                _fachada.AgregarCliente(cliente);
                await ReplyAsync($"Se agregó el cliente {cliente.Nombre} al sistema");
            }
            catch (CampoInvalidoExepcion e)
            {
                await ReplyAsync(e.Message);
            }
    }
}