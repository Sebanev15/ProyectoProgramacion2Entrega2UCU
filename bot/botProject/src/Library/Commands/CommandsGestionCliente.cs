using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

public class CommandsGestionCliente
{
    public class CommandAgregarCliente: ModuleBase<SocketCommandContext>
    {
        private readonly Fachada _fachada;

        CommandAgregarCliente(Fachada fachada)
        {
            _fachada = fachada;
        }

        [Command("agregarCliente")]
        public async Task ExecuteAsync(Cliente cliente)
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
    public class CommandEliminarCliente: ModuleBase<SocketCommandContext>
    {
        private readonly Fachada _fachada;

        CommandEliminarCliente(Fachada fachada)
        {
            _fachada = fachada;
        }

        [Command("eliminarCliente")]
        public async Task ExecuteAsync(Cliente cliente)
        {
            try
            {
                _fachada.EliminarCliente(cliente);
                await ReplyAsync($"Se eliminó el cliente {cliente.Nombre} del sistema");
            }
            catch (CampoInvalidoExepcion e)
            {
                await ReplyAsync(e.Message);
            }
        }
    }
    public class CommandModificarCliente: ModuleBase<SocketCommandContext>
    {
        private readonly Fachada _fachada;

        CommandModificarCliente(Fachada fachada)
        {
            _fachada = fachada;
        }

        [Command("modificarCliente")]
        public async Task ExecuteAsync(Cliente cliente, Cliente clienteModificado)
        {
            try
            {
                _fachada.ModificarCliente(cliente, clienteModificado);
                await ReplyAsync($"Se modificó el cliente {cliente.Nombre} usando al cliente {clienteModificado.Nombre}");
            } catch (CampoInvalidoExepcion e)
            {
                await ReplyAsync(e.Message);
            }
        }
    }
}