using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Interactions;
using Library;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

public class CommandsImporte: InteractionModuleBase<SocketInteractionContext>
{
        private readonly Fachada _fachada;

        public CommandsImporte(Fachada fachada)
        {
            _fachada = fachada;
        }
        

        [SlashCommand("crearcotizacion", "Crea una cotización")]
        public async Task ExecuteCrearCotizacionAsync(double monto, DateTime fecha, [Remainder] string parametrosCliente)
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
                $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
        }

        [SlashCommand("crearventa", "Crea una venta")]
        public async Task ExecuteCrearVentaAsync([Remainder] string producto,double monto, DateTime fecha, [Remainder] string parametrosCliente)
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
                $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
        }
        
        [SlashCommand("obtenerventastotales", "Devuelve todas las ventas en un periodo de tiempo")]
        public async Task ExecuteObtenerVentasTotalesAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<String> resultado = _fachada.ObtenerVentasTotales(fechaInicio,fechaFin);

                string mensaje = "Total de ventas:\n" + string.Join("\n", resultado.Select(venta => $"- {venta}"));

                await ReplyAsync(mensaje);
            }
            catch (ListaVaciaExcepcion e)
            {
                await ReplyAsync(e.Message);
            }
        }
    
}