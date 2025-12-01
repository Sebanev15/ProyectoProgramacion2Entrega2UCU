using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands
{
    public class CommandCrearVenta : ModuleBase<SocketCommandContext>
    {
        private readonly Fachada _fachada;

        public CommandCrearVenta(Fachada fachada)
        {
            _fachada = fachada;
        }

        [Command("crearVenta")]
        public async Task ExecuteAsync(string producto, double monto, DateTime fecha, [Remainder] string parametrosCliente)
        {
            List<string> parametros = parametrosCliente.Split(' ').ToList();
            var clientes = _fachada.BuscarCliente(parametros);
            
            if (clientes.Count == 0)
            {
                await ReplyAsync($"No se encontró ningún cliente con los parametros {parametros}.");
                return;
            }
            
            if (clientes.Count == 1)
            {
                var cliente = clientes.First();

                _fachada.CrearVenta(producto, fecha, monto, cliente);

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
                $"Se encontraron varios clientes con los parametros {parametros}.\n" +
                $"Elegí uno usando:\n`!elegirCliente <numero>`\n\n{listado}");
        }
    }
}