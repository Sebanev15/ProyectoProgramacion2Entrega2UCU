using Discord;
using Discord.Interactions;
using Library;
using System.Threading.Tasks;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

public class CommandAgregarClientePrueba : InteractionModuleBase<SocketInteractionContext>
{
    private readonly Fachada _fachada;

    public CommandAgregarClientePrueba(Fachada fachada)
    {
        _fachada = fachada;
    }

    [SlashCommand("agregarCliente", "Crear un cliente con formulario")]
    public async Task AbrirModalAsync(Cliente cliente)
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