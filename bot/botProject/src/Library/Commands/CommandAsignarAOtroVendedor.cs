namespace Ucu.Poo.DiscordBot.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Ucu.Poo.DiscordBot.Domain;

public class CommandAsignarAOtroVendedor : ModuleBase<SocketCommandContext>
{
    private readonly Fachada _fachada;

    CommandAsignarAOtroVendedor(Fachada fachada)
    {
        _fachada = fachada;
    }
    [Command("registrarUsuario")]
    public async Task ExecuteAsync(Vendedor vendedorInicial, Vendedor vendedorAsignado, Cliente cliente)
    {
        try
        {
            _fachada.AsignarOtroVendedor(vendedorInicial, vendedorAsignado, cliente);
            await ReplyAsync($"Se asigno el cliente {cliente.Nombre} al vendedor {vendedorAsignado.Nombre} desde el vendedor {vendedorInicial.Nombre}");
        }
        catch (CampoInvalidoExepcion e)
        {
            await ReplyAsync(e.Message);
        }
    }

}