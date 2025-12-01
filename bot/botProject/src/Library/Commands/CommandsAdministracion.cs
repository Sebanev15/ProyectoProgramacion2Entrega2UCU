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
    public async Task ExecuteReactivarUsuarioAsync( [Remainder] string parametrosAdmin, [Remainder] string parametrosUsuario)
    {
        List<string> parametrosUsuarios = parametrosUsuario.Split(' ').ToList();
        List<string> parametrosAdmins = parametrosAdmin.Split(' ').ToList();
        List<Usuario> usuarios = _fachada.BuscarUsuario(parametrosUsuarios);
        List<Usuario> admins = _fachada.BuscarUsuario(parametrosAdmins);
        
        if (usuarios.Count == 0 )
        {
            await ReplyAsync($"No se encontró ningún usuario con los parametros {parametrosUsuario}.");
            return;
        }
        if (admins.Count == 0 )
        {
            await ReplyAsync($"No se encontró ningún admin con los parametros {parametrosAdmin}.");
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
                $"Se encontraron varios admins con los parametros {parametrosAdmin}.\n" +
                $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
        }
        
        if (usuarios.Count > 1)
        {
            SeleccionesUsuarios.OpcionesUsuarios[Context.User.Id] = usuarios;
            
            var listado = string.Join("\n",
                usuarios.Select((c, i) => $"{i + 1}. {c.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varios admins con los parametros {parametrosUsuario}.\n" +
                $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
        }
    }
    

    [SlashCommand("registrarusuario","Registra al usuario, primero se le pasan los parametros de un Admin y despues los de un Usuario")]
    public async Task ExecuteRegistrarUsuarioAsync( [Remainder] string parametrosAdmin, [Remainder] string parametrosUsuario)
    {
        List<string> parametrosUsuarios = parametrosUsuario.Split(' ').ToList();
        List<string> parametrosAdmins = parametrosAdmin.Split(' ').ToList();
        List<Usuario> usuarios = _fachada.BuscarUsuario(parametrosUsuarios);
        List<Usuario> admins = _fachada.BuscarUsuario(parametrosAdmins);
        
        if (usuarios.Count == 0 )
        {
            await ReplyAsync($"No se encontró ningún usuario con los parametros {parametrosUsuarios}.");
            return;
        }
        if (admins.Count == 0 )
        {
            await ReplyAsync($"No se encontró ningún admin con los parametros {parametrosAdmin}.");
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
                $"Se encontraron varios admins con los parametros {parametrosAdmin}.\n" +
                $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
        }
        
        if (usuarios.Count > 1)
        {
            SeleccionesUsuarios.OpcionesUsuarios[Context.User.Id] = usuarios;
            
            var listado = string.Join("\n",
                usuarios.Select((c, i) => $"{i + 1}. {c.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varios usuarios con los parametros {parametrosUsuarios}.\n" +
                $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
        }
    }
    
    [SlashCommand("eliminarusuario", "Elimina el usuario. Se pasa primero un admin y despues los parametros del usuario")]
    public async Task ExecuteEliminarUsuarioAsync( [Remainder] string parametrosAdmin, [Remainder] string parametrosUsuario)
    {
        List<string> parametrosUsuarios = parametrosUsuario.Split(' ').ToList();
        List<string> parametrosAdmins = parametrosAdmin.Split(' ').ToList();
        List<Usuario> usuarios = _fachada.BuscarUsuario(parametrosUsuarios);
        List<Usuario> admins = _fachada.BuscarUsuario(parametrosAdmins);
        
        if (usuarios.Count == 0 )
        {
            await ReplyAsync($"No se encontró ningún usuario con los parametros {parametrosUsuario}.");
            return;
        }
        if (admins.Count == 0 )
        {
            await ReplyAsync($"No se encontró ningún admin con los parametros {parametrosAdmin}.");
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
                $"Se encontraron varios admins con los parametros {parametrosAdmin}.\n" +
                $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
        }
        
        if (usuarios.Count > 1)
        {
            SeleccionesUsuarios.OpcionesUsuarios[Context.User.Id] = usuarios;
            
            var listado = string.Join("\n",
                usuarios.Select((c, i) => $"{i + 1}. {c.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varios admins con los parametros {parametrosUsuario}.\n" +
                $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
        }
    }

    [SlashCommand("asignaraotrovendedor", "Asigna un cliente a otro vendedor.")]
    public async Task ExecuteAsignarAOtroVendedorAsync([Remainder] string parametrosVendedorInicial, [Remainder] string parametrosVendedorAsignado, [Remainder] string parametrosCliente)
    {
        {
            List<string> parametrosClientes = parametrosCliente.Split(' ').ToList();
            List<string> parametrosVendedoresAsignados = parametrosVendedorAsignado.Split(' ').ToList();
            List<string> parametrosVendedoresIniciales = parametrosVendedorInicial.Split(' ').ToList();
            List<Usuario> vendedoresIniciales = _fachada.BuscarUsuario(parametrosVendedoresIniciales);
            List<Usuario> VendedoresAsignados = _fachada.BuscarUsuario(parametrosVendedoresAsignados);
            List<Cliente> clientes = _fachada.BuscarCliente(parametrosClientes);
        
            if (vendedoresIniciales.Count == 0 )
            {
                await ReplyAsync($"No se encontró ningún vendedor con los parametros {parametrosVendedorInicial}.");
                return;
            }
            if (VendedoresAsignados.Count == 0 )
            {
                await ReplyAsync($"No se encontró ningún vendedor con los parametros {parametrosVendedorAsignado}.");
                return;
            }
            if (clientes.Count == 0 )
            {
                await ReplyAsync($"No se encontró ningún cliente con los parametros {parametrosCliente}.");
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
                    $"Se encontraron varios admins con los parametros {parametrosVendedorInicial}.\n" +
                    $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
            }
        
            if (VendedoresAsignados.Count > 1)
            {
                SeleccionesUsuarios.OpcionesUsuarios[Context.User.Id] = VendedoresAsignados;
            
                var listado = string.Join("\n",
                    VendedoresAsignados.Select((c, i) => $"{i + 1}. {c.Nombre}"));

                await ReplyAsync(
                    $"Se encontraron varios admins con los parametros {parametrosVendedorAsignado}.\n" +
                    $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
            }            
            
            if (clientes.Count > 1)
            {
                SeleccionesUsuarios.OpcionesClientes[Context.User.Id] = clientes;
            
                var listado = string.Join("\n",
                    clientes.Select((c, i) => $"{i + 1}. {c.Nombre}"));

                await ReplyAsync(
                    $"Se encontraron varios admins con los parametros {parametrosCliente}.\n" +
                    $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
            }
        }
    }
}