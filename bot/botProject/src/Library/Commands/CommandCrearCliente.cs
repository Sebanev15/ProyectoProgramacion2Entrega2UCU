using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Ucu.Poo.DiscordBot.Domain;

public class CommandCrearCliente: ModuleBase<SocketCommandContext>
{
    private readonly Fachada _fachada;

    CommandCrearCliente(Fachada fachada)
    {
        _fachada = fachada;
    }

    [Command("crearCliente")]
    public async Task ExecuteAsync([Remainder] string nombre, [Remainder] string apellido, string telefono, string correo, string genero, DateTime fechaDeNacimiento)
    {
        try
        {
            genero = genero.ToUpper();
            _fachada.CrearCliente(nombre, apellido, telefono, correo, genero, fechaDeNacimiento);
            await ReplyAsync($"Se creo el cliente {nombre}");
        }
        catch (CampoInvalidoExepcion e)
        {
            await ReplyAsync(e.Message);
        }
    }
}