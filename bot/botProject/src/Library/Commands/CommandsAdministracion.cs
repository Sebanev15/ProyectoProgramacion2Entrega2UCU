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
                $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
        }
        
        if (usuarios.Count > 1)
        {
            SeleccionesUsuarios.OpcionesUsuarios[Context.User.Id] = usuarios;
            
            var listado = string.Join("\n",
                usuarios.Select((c, i) => $"{i + 1}. {c.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varios admins con el nombre {nombreUsuario}.\n" +
                $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
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
            
            _fachada.RegistrarUsuario(admin,usuario);
            await ReplyAsync($"Se registro al usuario {usuario.Nombre} en el sistema");
            return;
        }

        if (admins.Count > 1)
        {
            SeleccionesUsuarios.OpcionesUsuarios[Context.User.Id] = admins;
            
            var listado = string.Join("\n",
                admins.Select((c, i) => $"{i + 1}. {c.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varios admins con el nombre {nombreAdmin}.\n" +
                $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
        }
        
        if (usuarios.Count > 1)
        {
            SeleccionesUsuarios.OpcionesUsuarios[Context.User.Id] = usuarios;
            
            var listado = string.Join("\n",
                usuarios.Select((c, i) => $"{i + 1}. {c.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varios admins con el nombre {nombreUsuario}.\n" +
                $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
        }
    }
    
    [SlashCommand("eliminarusuario", "Elimina el usuario")]
    public async Task ExecuteEliminarUsuarioAsync(string nombreAdmin, string nombreUsuario)
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
            
            _fachada.EliminarUsuario(admin,usuario);
            await ReplyAsync($"Se registro al usuario {usuario.Nombre} en el sistema");
            return;
        }

        if (admins.Count > 1)
        {
            SeleccionesUsuarios.OpcionesUsuarios[Context.User.Id] = admins;
            
            var listado = string.Join("\n",
                admins.Select((c, i) => $"{i + 1}. {c.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varios admins con el nombre {nombreAdmin}.\n" +
                $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
        }
        
        if (usuarios.Count > 1)
        {
            SeleccionesUsuarios.OpcionesUsuarios[Context.User.Id] = usuarios;
            
            var listado = string.Join("\n",
                usuarios.Select((c, i) => $"{i + 1}. {c.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varios admins con el nombre {nombreUsuario}.\n" +
                $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
        }
    }

    [SlashCommand("asignaraotrovendedor", "Asigna un cliente a otro vendedor")]
    public async Task ExecuteAsignarAOtroVendedorAsync(string nombreVendedorInicial, string nombreVendedorAsignado, string nombreCliente)
    {
        {
            List<Usuario> vendedoresIniciales = _fachada.BuscarUsuario(nombreVendedorInicial);
            List<Usuario> VendedoresAsignados = _fachada.BuscarUsuario(nombreVendedorAsignado);
            List<Cliente> clientes = _fachada.BuscarCliente(nombreCliente);
        
            if (vendedoresIniciales.Count == 0 )
            {
                await ReplyAsync($"No se encontró ningún vendedor llamado {nombreVendedorInicial}.");
                return;
            }
            if (VendedoresAsignados.Count == 0 )
            {
                await ReplyAsync($"No se encontró ningún vendedor llamado {nombreVendedorAsignado}.");
                return;
            }
            if (clientes.Count == 0 )
            {
                await ReplyAsync($"No se encontró ningún cliente llamado {nombreCliente}.");
                return;
            }
            if (vendedoresIniciales.Count == 1 && VendedoresAsignados.Count == 1 && clientes.Count == 1)
            {
                Vendedor vendedorInicial = (Vendedor)vendedoresIniciales.First();
                Vendedor vendedorAsignado = (Vendedor)VendedoresAsignados.First();
                Cliente cliente = clientes.First();
            
                _fachada.AsignarOtroVendedor(vendedorInicial,vendedorAsignado,cliente);
                await ReplyAsync($"Se asigno el cliente {cliente.Nombre} al vendedor {vendedorAsignado.Nombre} y ya no es cliente de {vendedorInicial.Nombre}");
                return;
            }

            if (vendedoresIniciales.Count > 1)
            {
                SeleccionesUsuarios.OpcionesUsuarios[Context.User.Id] = vendedoresIniciales;
            
                var listado = string.Join("\n",
                    vendedoresIniciales.Select((c, i) => $"{i + 1}. {c.Nombre}"));

                await ReplyAsync(
                    $"Se encontraron varios admins con el nombre {nombreVendedorInicial}.\n" +
                    $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
            }
        
            if (VendedoresAsignados.Count > 1)
            {
                SeleccionesUsuarios.OpcionesUsuarios[Context.User.Id] = VendedoresAsignados;
            
                var listado = string.Join("\n",
                    VendedoresAsignados.Select((c, i) => $"{i + 1}. {c.Nombre}"));

                await ReplyAsync(
                    $"Se encontraron varios admins con el nombre {nombreVendedorAsignado}.\n" +
                    $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
            }            
            
            if (clientes.Count > 1)
            {
                SeleccionesUsuarios.OpcionesClientes[Context.User.Id] = clientes;
            
                var listado = string.Join("\n",
                    clientes.Select((c, i) => $"{i + 1}. {c.Nombre}"));

                await ReplyAsync(
                    $"Se encontraron varios admins con el nombre {nombreCliente}.\n" +
                    $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
            }
        }
    }
}