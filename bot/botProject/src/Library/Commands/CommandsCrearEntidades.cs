using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Interactions;
using Library;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

public class CommandsCrearEntidades
{
    public class CommandCrearEtiqueta : ModuleBase<SocketCommandContext>
    {
        private readonly Fachada _fachada;

        CommandCrearEtiqueta(Fachada fachada)
        {
            _fachada = fachada;
        }
        [SlashCommand("crearetiqueta", "Crea una etiqueta")]
        public async Task ExecuteAsync(string nombre)
        {
            try
            {
                _fachada.CrearEtiqueta(nombre);
                await ReplyAsync($"Se creó la etiqueta {nombre}");
            }
            catch (CampoInvalidoExepcion e)
            {
                await ReplyAsync(e.Message);
            }
        }

    }
    
    public class CommandCrearCotizacion : ModuleBase<SocketCommandContext>
    {
        private readonly Fachada _fachada;

        public CommandCrearCotizacion(Fachada fachada)
        {
            _fachada = fachada;
        }

        [SlashCommand("crearcotizacion", "Crea una cotización")]
        
        public async Task ExecuteAsync(double monto, DateTime fecha, [Remainder] string nombreCliente)
        {
            var clientes = _fachada.BuscarCliente(nombreCliente);
            
            if (clientes.Count == 0)
            {
                await ReplyAsync($"No se encontró ningún cliente llamado {nombreCliente}.");
                return;
            }
            
            if (clientes.Count == 1)
            {
                var cliente = clientes.First();

                _fachada.CrearCotizacion(fecha, monto, cliente);

                await ReplyAsync(
                    $"Cotización creada para {cliente.Nombre}\n" +
                    $"Monto: ${monto}\n" +
                    $"Fecha: {fecha:dd-MM-yyyy}");
                return;
            }
            
            SeleccionesUsuarios.OpcionesClientes[Context.User.Id] = clientes;

            var listado = string.Join("\n",
                clientes.Select((c, i) => $"{i + 1}. {c.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varios clientes con el nombre {nombreCliente}.\n" +
                $"Elegí uno usando:\n`!elegirCliente <numero>`\n\n{listado}");
        }
    }
    
    public class CommandCrearCliente: ModuleBase<SocketCommandContext>
    {
        private readonly Fachada _fachada;

        CommandCrearCliente(Fachada fachada)
        {
            _fachada = fachada;
        }

        [SlashCommand("crearcliente", "Crea un cliente")]
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
    
    public class CommandCrearVenta : ModuleBase<SocketCommandContext>
    {
        private readonly Fachada _fachada;

        public CommandCrearVenta(Fachada fachada)
        {
            _fachada = fachada;
        }

        [SlashCommand("crearventa", "Crea una venta")]
        
        public async Task ExecuteAsync([Remainder] string producto,double monto, DateTime fecha, [Remainder] string nombreCliente)
        {
            var clientes = _fachada.BuscarCliente(nombreCliente);
            
            if (clientes.Count == 0)
            {
                await ReplyAsync($"No se encontró ningún cliente llamado {nombreCliente}.");
                return;
            }
            
            if (clientes.Count == 1)
            {
                var cliente = clientes.First();

                _fachada.CrearVenta(producto,fecha, monto, cliente);

                await ReplyAsync(
                    $"Venta creada para {cliente.Nombre}\n" +
                    $"Monto: ${monto}\n" +
                    $"Fecha: {fecha:dd-MM-yyyy}");
                return;
            }
            
            SeleccionesUsuarios.OpcionesClientes[Context.User.Id] = clientes;

            var listado = string.Join("\n",
                clientes.Select((c, i) => $"{i + 1}. {c.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varios clientes con el nombre {nombreCliente}.\n" +
                $"Elegí uno usando:\n`!elegirCliente <numero>`\n\n{listado}");
        }
    }
}