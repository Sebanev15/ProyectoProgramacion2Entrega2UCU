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
        if (nombre != null && apellido != null && telefono != null && correo != null && genero != null &&
            fechaDeNacimiento != null)
        {
            if (genero.ToUpper() == "M" || genero.ToUpper() == "H")
            {
                genero = genero.ToUpper();
                _fachada.CrearCliente(nombre, apellido, telefono, correo, genero, fechaDeNacimiento);
                await ReplyAsync($"Se creo el cliente {nombre}");
            }
            else
            {
                await ReplyAsync($"Genero invalido");
            }
        }
        else
        {
            await ReplyAsync($"No pueden haver campos vacios");
        }
        
    }
}