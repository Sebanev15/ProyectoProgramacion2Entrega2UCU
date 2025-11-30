using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

public class CommandsAdministracion
{
    public class CommandReactivarUsuario: ModuleBase<SocketCommandContext>
    {
        private readonly Fachada _fachada;

        CommandReactivarUsuario(Fachada fachada)
        {
            _fachada = fachada;
        }

        [Command("reactivarUsuario")]
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
    }
    
    public class CommandReistrarUsuario : ModuleBase<SocketCommandContext>
    {
        private readonly Fachada _fachada;

        CommandReistrarUsuario(Fachada fachada)
        {
            _fachada = fachada;
        }
        [Command("registrarUsuario")]
        public async Task ExecuteAsync(Administrador administrador, Usuario usuario)
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
        
        public class CommandSuspenderUsuario: ModuleBase<SocketCommandContext>
        {
            private readonly Fachada _fachada;

            CommandSuspenderUsuario(Fachada fachada)
            {
                _fachada = fachada;
            }

            [Command("suspenderUsuario")]
            public async Task ExecuteAsync(Administrador admin, Usuario usuario)
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
        }
        
        public class CommandEliminarUsuario : ModuleBase<SocketCommandContext>
        {
            private readonly Fachada _fachada;

            CommandEliminarUsuario(Fachada fachada)
            {
                _fachada = fachada;
            }
            [Command("registrarUsuario")]
            public async Task ExecuteAsync(Administrador administrador, Usuario usuario)
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

        }
        
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
    }
}