/*
namespace Ucu.Poo.DiscordBot.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Ucu.Poo.DiscordBot.Domain;

public class CommandModificarCliente: ModuleBase<SocketCommandContext>
{
    private readonly Fachada _fachada;

    CommandModificarCliente(Fachada fachada)
    {
        _fachada = fachada;
    }

    [Command("modificarCliente")]
    public async Task ExecuteAsync(Cliente cliente, Cliente clienteModificado)
    {
        try
        {
            _fachada.ModificarCliente(cliente, clienteModificado);
            await ReplyAsync($"Se modificó el cliente {cliente.Nombre} usando al cliente {clienteModificado.Nombre}");
        } catch (CampoInvalidoExepcion e)
        {
            await ReplyAsync(e.Message);
        }
    }
}
*/