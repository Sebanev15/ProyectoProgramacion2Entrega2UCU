using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Interactions;
using Library;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

public class CommandsUsuario : InteractionModuleBase<SocketInteractionContext>
{

    private readonly Fachada _fachada;

    public CommandsUsuario(Fachada fachada)
    {
        _fachada = fachada;
    }

    
    [SlashCommand("reactivarusuario", "Reactiva al usuario")]
    public async Task ExecuteReactivarUsuarioAsync(string parametrosAdmin, string parametrosUsuario)
    {
        List<string> parametrosUsuarios = parametrosUsuario.Split(' ').ToList();
        List<string> parametrosAdmins = parametrosAdmin.Split(' ').ToList();

        List<Usuario> usuarios = _fachada.BuscarUsuario(parametrosUsuarios);
        List<Administrador> admins = _fachada.BuscarUsuario(parametrosAdmins).OfType<Administrador>().ToList();


        if (Selecciones.AdminSeleccionado.TryGetValue(Context.User.Id, out Administrador adminSel) &&
            Selecciones.UsuarioSeleccionado.TryGetValue(Context.User.Id, out Usuario usuarioSel))
        {
            _fachada.ReactivarUsuario(adminSel, usuarioSel);

            await ReplyAsync($"Se reactivó el usuario {usuarioSel.Nombre} correctamente.");


            Selecciones.AdminSeleccionado.Remove(Context.User.Id);
            Selecciones.UsuarioSeleccionado.Remove(Context.User.Id);
            return;
        }
        
        if (usuarios.Count == 0)
        {
            await ReplyAsync($"No se encontró ningún usuario con los parámetros: {parametrosUsuario}");
            return;
        }

        if (admins.Count == 0)
        {
            await ReplyAsync($"No se encontró ningún admin con los parámetros: {parametrosAdmin}");
            return;
        }
        
        if (usuarios.Count == 1 && admins.Count == 1)
        {
            Administrador admin = (Administrador)admins.First();
            Usuario usuario = usuarios.First();

            _fachada.ReactivarUsuario(admin, usuario);

            await ReplyAsync($"Se reactivó el usuario {usuario.Nombre}");
            return;
        }
        
        if (admins.Count > 1)
        {
            Selecciones.OpcionesAdmins[Context.User.Id] = admins;

            string listado = string.Join("\n",
                admins.Select((a, i) =>
                    $"{i + 1}. {a.Nombre} - {a.Correo} - {a.Telefono}"));

            await ReplyAsync(
                $"Se encontraron varios admins.\n" +
                $"Elegí uno usando `/elegiradmin <numero>`:\n\n{listado}");
        }
        
        if (usuarios.Count > 1)
        {
            Selecciones.OpcionesUsuarios[Context.User.Id] = usuarios;

            string listado = string.Join("\n",
                usuarios.Select((u, i) =>
                    $"{i + 1}. {u.Nombre} - {u.Correo} - {u.Telefono}"));

            await ReplyAsync(
                $"Se encontraron varios usuarios.\n" +
                $"Elegí uno usando `/elegirusuario <numero>`:\n\n{listado}");
        }
    }

    

    [SlashCommand("registrarusuario","Registra al usuario; primero un Admin y luego el Usuario.")]
    public async Task ExecuteRegistrarUsuarioAsync(string parametrosAdmin, string parametrosUsuario)
    {
        List<string> paramsUsuarios = parametrosUsuario.Split(' ').ToList();
        List<string> paramsAdmins = parametrosAdmin.Split(' ').ToList();
        
        List<Usuario> usuarios = _fachada.BuscarUsuario(paramsUsuarios);
        List<Administrador> admins = _fachada.BuscarUsuario(paramsAdmins).OfType<Administrador>().ToList();
        

        if (Selecciones.AdminSeleccionado.TryGetValue(Context.User.Id, out Administrador adminSel) &&
            Selecciones.UsuarioSeleccionado.TryGetValue(Context.User.Id, out Usuario usuarioSel))
        {
            _fachada.RegistrarUsuario(adminSel, usuarioSel);

            await ReplyAsync($"Se registró al usuario {usuarioSel.Nombre} correctamente.");


            Selecciones.AdminSeleccionado.Remove(Context.User.Id);
            Selecciones.UsuarioSeleccionado.Remove(Context.User.Id);

            return;
        }

        if (admins.Count == 0)
        {
            await ReplyAsync($"No se encontró ningún admin con los parámetros {parametrosAdmin}");
            return;
        }
        if (usuarios.Count == 0)
        {
            await ReplyAsync($"No se encontró ningún usuario con los parámetros {parametrosUsuario}");
            return;
        }


        if (admins.Count == 1 && usuarios.Count == 1)
        {
            Usuario userTmp = usuarios.First();
            Usuario adminTmp = admins.First();

            if (adminTmp is not Administrador adminFinal)
            {
                await ReplyAsync("El usuario encontrado no es un administrador.");
                return;
            }

            _fachada.RegistrarUsuario(adminFinal, userTmp);
            await ReplyAsync($"Se registró al usuario {userTmp.Nombre}");
            return;
        }


        if (admins.Count > 1)
        {
            Selecciones.OpcionesAdmins[Context.User.Id] = admins;

            string listado = string.Join("\n",
                admins.Select((a, i) => $"{i + 1}. {a.Nombre} - {a.Correo}"));

            await ReplyAsync(
                $"Se encontraron varios ADMIN con {parametrosAdmin}.\n" +
                $"Elegí uno:\n`/elegiradmin <numero>`\n\n{listado}"
            );
        }


        if (usuarios.Count > 1)
        {
            Selecciones.OpcionesUsuarios[Context.User.Id] = usuarios;

            string listado = string.Join("\n",
                usuarios.Select((u, i) => $"{i + 1}. {u.Nombre} - {u.Correo}"));

            await ReplyAsync(
                $"Se encontraron varios USUARIOS con {parametrosUsuario}.\n" +
                $"Elegí uno:\n`/elegirusuario <numero>`\n\n{listado}"
            );
        }
    }

    
    [SlashCommand("eliminarusuario", "Elimina un usuario. Primero un admin, luego el usuario a eliminar.")]
    public async Task ExecuteEliminarUsuarioAsync(string parametrosAdmin, string parametrosUsuario)
    {
        List<string> adminParams = parametrosAdmin.Split(' ').ToList();
        List<string> usuarioParams = parametrosUsuario.Split(' ').ToList();

        List<Administrador> admins = _fachada.BuscarUsuario(adminParams).OfType<Administrador>().ToList();
        List<Usuario> usuarios = _fachada.BuscarUsuario(usuarioParams);
        
        
        if (Selecciones.AdminSeleccionado.TryGetValue(Context.User.Id, out Administrador adminSel) &&
            Selecciones.UsuarioSeleccionado.TryGetValue(Context.User.Id, out Usuario usuarioSel))
        {
            Administrador admin = (Administrador)adminSel;

            _fachada.EliminarUsuario(admin, usuarioSel);

            await ReplyAsync($"El usuario **{usuarioSel.Nombre}** fue eliminado correctamente por **{admin.Nombre}**.");


            Selecciones.AdminSeleccionado.Remove(Context.User.Id);
            Selecciones.UsuarioSeleccionado.Remove(Context.User.Id);
            return;
        }


        if (admins.Count == 0)
        {
            await ReplyAsync($"No se encontró ningún admin con: {parametrosAdmin}.");
            return;
        }

        if (usuarios.Count == 0)
        {
            await ReplyAsync($"No se encontró ningún usuario con: {parametrosUsuario}.");
            return;
        }
        
        if (admins.Count == 1 && usuarios.Count == 1)
        {
            Administrador admin = (Administrador)admins.First();
            Usuario usuario = usuarios.First();

            _fachada.EliminarUsuario(admin, usuario);

            await ReplyAsync($"El usuario **{usuario.Nombre}** fue eliminado correctamente.");
            return;
        }


        if (admins.Count > 1)
        {
            Selecciones.OpcionesAdmins[Context.User.Id] = admins;

            string listadoAdmins = string.Join("\n",
                admins.Select((a, i) => $"{i + 1}. {a.Nombre} - {a.Correo} ({a.Telefono})"));

            await ReplyAsync(
                $"Se encontraron varios admins con: `{parametrosAdmin}`.\n" +
                $"Elegí uno con:\n`/elegiradmin <numero>`\n\n{listadoAdmins}");
        }
        
        if (usuarios.Count > 1)
        {
            Selecciones.OpcionesUsuarios[Context.User.Id] = usuarios;

            string listadoUsuarios = string.Join("\n",
                usuarios.Select((u, i) => $"{i + 1}. {u.Nombre} - {u.Correo} ({u.Telefono})"));

            await ReplyAsync(
                $"Se encontraron varios usuarios con: `{parametrosUsuario}`.\n" +
                $"Elegí uno con:\n`/elegirusuario <numero>`\n\n{listadoUsuarios}");
        }
    }


    [SlashCommand("asignaraotrovendedor", "Asigna un cliente a otro vendedor.")]
    public async Task ExecuteAsignarAOtroVendedorAsync([Remainder] string parametrosVendedorInicial, [Remainder] string parametrosVendedorAsignado, [Remainder] string parametrosCliente)
    {
        
        List<string> parametrosClientes = parametrosCliente.Split(' ').ToList();
        List<string> parametrosVendedoresAsignados = parametrosVendedorAsignado.Split(' ').ToList();
        List<string> parametrosVendedoresIniciales = parametrosVendedorInicial.Split(' ').ToList();
        
        List<Usuario> vendedoresIniciales = _fachada.BuscarUsuario(parametrosVendedoresIniciales);
        List<Usuario> vendedoresAsignados = _fachada.BuscarUsuario(parametrosVendedoresAsignados);
        List<Cliente> clientes = _fachada.BuscarCliente(parametrosClientes);


        bool tieneCliente = Selecciones.ClienteSeleccionado.TryGetValue(Context.User.Id, out Cliente clienteSel);
        bool tieneInicial = Selecciones.VendedorInicialSeleccionado.TryGetValue(Context.User.Id, out Usuario inicialSel);
        bool tieneAsignado = Selecciones.VendedorAsignadoSeleccionado.TryGetValue(Context.User.Id, out Usuario asignadoSel);

        if (tieneCliente && tieneInicial && tieneAsignado)
        {
            _fachada.AsignarOtroVendedor((Vendedor)inicialSel, (Vendedor)asignadoSel, clienteSel);

            await ReplyAsync($"Cliente {clienteSel.Nombre} reasignado de {inicialSel.Nombre} a {asignadoSel.Nombre}");
            
            Selecciones.ClienteSeleccionado.Remove(Context.User.Id);
            Selecciones.VendedorInicialSeleccionado.Remove(Context.User.Id);
            Selecciones.VendedorAsignadoSeleccionado.Remove(Context.User.Id);
            Selecciones.OpcionesClientes.Remove(Context.User.Id);
            Selecciones.OpcionesVendedorInicial.Remove(Context.User.Id);
            Selecciones.OpcionesVendedorAsignado.Remove(Context.User.Id);
            return;
        }
        
        if (vendedoresIniciales.Count == 0)
        {
            await ReplyAsync($"No se encontró ningún vendedor con los parámetros: {parametrosVendedorInicial}");
            return;
        }
        if (vendedoresAsignados.Count == 0)
        {
            await ReplyAsync($"No se encontró ningún vendedor con los parámetros: {parametrosVendedorAsignado}");
            return;
        }
        if (clientes.Count == 0)
        {
            await ReplyAsync($"No se encontró ningún cliente con los parámetros: {parametrosCliente}");
            return;
        }

        if (vendedoresIniciales.Count == 1)
            Selecciones.VendedorInicialSeleccionado[Context.User.Id] = vendedoresIniciales.First();

        if (vendedoresAsignados.Count == 1)
            Selecciones.VendedorAsignadoSeleccionado[Context.User.Id] = vendedoresAsignados.First();

        if (clientes.Count == 1)
            Selecciones.ClienteSeleccionado[Context.User.Id] = clientes.First();

        bool listo = 
            Selecciones.ClienteSeleccionado.ContainsKey(Context.User.Id) &&
            Selecciones.VendedorInicialSeleccionado.ContainsKey(Context.User.Id) &&
            Selecciones.VendedorAsignadoSeleccionado.ContainsKey(Context.User.Id);

        if (listo)
        {
            Cliente c = Selecciones.ClienteSeleccionado[Context.User.Id];
            Vendedor vin = (Vendedor)Selecciones.VendedorInicialSeleccionado[Context.User.Id];
            Vendedor vas = (Vendedor)Selecciones.VendedorAsignadoSeleccionado[Context.User.Id];

            _fachada.AsignarOtroVendedor(vin, vas, c);

            await ReplyAsync($"Cliente {c.Nombre} reasignado de {vin.Nombre} a {vas.Nombre}");
            
            Selecciones.ClienteSeleccionado.Remove(Context.User.Id);
            Selecciones.VendedorInicialSeleccionado.Remove(Context.User.Id);
            Selecciones.VendedorAsignadoSeleccionado.Remove(Context.User.Id);
            return;
        }
        
        
        if (vendedoresIniciales.Count > 1)
        {
            Selecciones.OpcionesVendedorInicial[Context.User.Id] = vendedoresIniciales;

            string listado = string.Join("\n",
                vendedoresIniciales.Select((v, i) =>
                    $"{i + 1}. Nombre: {v.Nombre}  Telefono:  {v.Correo}  Correo: {v.Telefono}"));

            await ReplyAsync(
                $"Hay varios vendedores iniciales que coinciden con {parametrosVendedorInicial}.\n" +
                $"Elegí uno usando:\n`/elegirvendedorinicial <n>`\n\n{listado}");
        }
        
        if (vendedoresAsignados.Count > 1)
        {
            Selecciones.OpcionesVendedorAsignado[Context.User.Id] = vendedoresAsignados;

            string listado = string.Join("\n",
                vendedoresAsignados.Select((v, i) =>
                    $"{i + 1}. Nombre: {v.Nombre}  Telefono:  {v.Correo}  Correo: {v.Telefono}"));

            await ReplyAsync(
                $"Hay varios vendedores asignados que coinciden con {parametrosVendedorAsignado}.\n" +
                $"Elegí uno usando:\n`/elegirvendedorasignado <n>`\n\n{listado}");
        }


        if (clientes.Count > 1)
        {
            Selecciones.OpcionesClientes[Context.User.Id] = clientes;

            string listado = string.Join("\n",
                clientes.Select((c, i) =>
                    $"{i + 1}. Nombre: {c.Nombre} Apellido: {c.Apellido} Correo: {c.Correo} Telefono: {c.Telefono}"));

            await ReplyAsync(
                $"Hay varios clientes que coinciden con {parametrosCliente}.\n" +
                $"Elegí uno usando:\n`/elegircliente <n>`\n\n{listado}");
        }
    }

}