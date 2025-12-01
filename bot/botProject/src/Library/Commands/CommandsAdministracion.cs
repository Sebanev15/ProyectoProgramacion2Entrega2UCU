using System.Threading.Tasks;
using Discord.Commands;
using Discord.Interactions;
using Library;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

public class CommandsAdministracion : InteractionModuleBase<SocketInteractionContext>
{

    private readonly Fachada _fachada;

    public CommandsAdministracion(Fachada fachada)
    {
        _fachada = fachada;
    }

    
    [SlashCommand("reactivarusuario", "Reactiva al usuario")]
    public async Task ExecuteAsync(Administrador admin, Usuario usuario)
    {
        try
        {
            _fachada.ReactivarUsuario(admin,usuario);
            await ReplyAsync($"Se reactivo el usuario {usuario.Nombre}");
        }
        catch (CampoInvalidoExepcion e)
        {
            await ReplyAsync(e.Message);
        }
    }

    [SlashCommand("registrarusuario","Registra al usuario")]
    public async Task ExecuteAsync1(Administrador administrador, Usuario usuario)
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


    [SlashCommand("suspenderusuario","Suspende al usuario")]
    public async Task ExecuteAsync2(Administrador admin, Usuario usuario)
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
    
    [SlashCommand("eliminarusuario", "Elimina el usuario")]
    public async Task ExecuteAsync3(Administrador administrador, Usuario usuario)
    {
        try
        {
            _fachada.EliminarUsuario(administrador, usuario);
            await ReplyAsync($"Se elimino el usuario {usuario.Nombre}");
        }
        catch (CampoInvalidoExepcion e)
        {
            await ReplyAsync(e.Message);
        }
    }

    [SlashCommand("asignaraotrovendedor", "Asigna un cliente a otro vendedor")]
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