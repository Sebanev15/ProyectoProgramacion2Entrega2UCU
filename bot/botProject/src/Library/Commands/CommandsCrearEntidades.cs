using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Interactions;
using Library;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

public class CommandsCrearEntidades: InteractionModuleBase<SocketInteractionContext>
{
        private readonly Fachada _fachada;

        public CommandsCrearEntidades(Fachada fachada)
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

        [SlashCommand("crearcotizacion", "Crea una cotización")]
        public async Task ExecuteAsync2(double monto, DateTime fecha, [Remainder] string parametrosCliente)
        {
            List<string> parametros = parametrosCliente.Split(' ').ToList();
            List<Cliente> clientes = _fachada.BuscarCliente(parametros);
            
            if (clientes.Count == 0)
            {
                await ReplyAsync($"No se encontró ningún cliente con los parametros {parametrosCliente}.");
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
                $"Se encontraron varios clientes con los parametros {parametrosCliente}.\n" +
                $"Elegí uno usando:\n`!elegirCliente <numero>`\n\n{listado}");
        }

        [SlashCommand("crearcliente", "Crea un cliente")]
        public async Task ExecuteAsync3([Remainder] string nombre, [Remainder] string apellido, string telefono, string correo, string genero, DateTime fechaDeNacimiento)
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

        [SlashCommand("crearventa", "Crea una venta")]
        public async Task ExecuteAsync4([Remainder] string producto,double monto, DateTime fecha, [Remainder] string parametrosCliente)
        {
            List<string> parametros = parametrosCliente.Split(' ').ToList();
            List<Cliente> clientes = _fachada.BuscarCliente(parametros);
            
            if (clientes.Count == 0)
            {
                await ReplyAsync($"No se encontró ningún cliente con los parametros {parametrosCliente}.");
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
                $"Se encontraron varios clientes con los parametros {parametrosCliente}.\n" +
                $"Elegí uno usando:\n`!elegirCliente <numero>`\n\n{listado}");
        }
    
}