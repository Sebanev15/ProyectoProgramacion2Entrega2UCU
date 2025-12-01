using System.Collections.Generic;
using System.Linq;
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
    public async Task ExecuteReactivarUsuarioAsync(string nombreAdmin, string nombreUsuario)
    {
        List<Usuario> usuarios = _fachada.BuscarUsuario(nombreUsuario);
        List<Usuario> admins = _fachada.BuscarUsuario(nombreAdmin);
        
        if (usuarios.Count == 0 )
        {
            await ReplyAsync($"No se encontró ningún usuario llamado {nombreUsuario}.");
            return;
        }
        if (admins.Count == 0 )
        {
            await ReplyAsync($"No se encontró ningún admin llamado {nombreAdmin}.");
            return;
        }
        if (usuarios.Count == 1 && admins.Count == 1)
        {
            Usuario usuario = usuarios.First();
            Administrador admin = (Administrador)admins.First();
            
            _fachada.ReactivarUsuario(admin,usuario);
            await ReplyAsync($"Se reactivo el usuario {usuario.Nombre}");
            return;
        }

        if (admins.Count > 1)
        {
            SeleccionesUsuarios.OpcionesUsuarios[Context.User.Id] = admins;
            
            var listado = string.Join("\n",
                admins.Select((c, i) => $"{i + 1}. {c.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varios admins con el nombre {nombreAdmin}.\n" +
                $"Elegí uno usando:\n`!elegirCliente <numero>`\n\n{listado}");
        }
        
        if (usuarios.Count > 1)
        {
            SeleccionesUsuarios.OpcionesUsuarios[Context.User.Id] = usuarios;
            
            var listado = string.Join("\n",
                usuarios.Select((c, i) => $"{i + 1}. {c.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varios admins con el nombre {nombreUsuario}.\n" +
                $"Elegí uno usando:\n`!elegirCliente <numero>`\n\n{listado}");
        }
    }
    

    [SlashCommand("registrarusuario","Registra al usuario")]
    public async Task ExecuteRegistrarUsuarioAsync(string nombreAdmin, string nombreUsuario)
    {
        List<Usuario> usuarios = _fachada.BuscarUsuario(nombreUsuario);
        List<Usuario> admins = _fachada.BuscarUsuario(nombreAdmin);
        
        if (usuarios.Count == 0 )
        {
            await ReplyAsync($"No se encontró ningún usuario llamado {nombreUsuario}.");
            return;
        }
        if (admins.Count == 0 )
        {
            await ReplyAsync($"No se encontró ningún admin llamado {nombreAdmin}.");
            return;
        }
        if (usuarios.Count == 1 && admins.Count == 1)
        {
            Usuario usuario = usuarios.First();
            Administrador admin = (Administrador)admins.First();
            
            _fachada.ReactivarUsuario(admin,usuario);
            await ReplyAsync($"Se reactivo el usuario {usuario.Nombre}");
            return;
        }

        if (admins.Count > 1)
        {
            SeleccionesUsuarios.OpcionesUsuarios[Context.User.Id] = admins;
            
            var listado = string.Join("\n",
                admins.Select((c, i) => $"{i + 1}. {c.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varios admins con el nombre {nombreAdmin}.\n" +
                $"Elegí uno usando:\n`!elegirCliente <numero>`\n\n{listado}");
        }
        
        if (usuarios.Count > 1)
        {
            SeleccionesUsuarios.OpcionesUsuarios[Context.User.Id] = usuarios;
            
            var listado = string.Join("\n",
                usuarios.Select((c, i) => $"{i + 1}. {c.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varios admins con el nombre {nombreUsuario}.\n" +
                $"Elegí uno usando:\n`!elegirCliente <numero>`\n\n{listado}");
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