using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands
{
    public class CommandCrearCotizacion : ModuleBase<SocketCommandContext>
    {
        private readonly Fachada _fachada;

        public CommandCrearCotizacion(Fachada fachada)
        {
            _fachada = fachada;
        }

        [Command("crearCotizacion")]
        
        public async Task ExecuteAsync(double monto, DateTime fecha, [Remainder] List<string> datosCliente)
        {
            var clientes = _fachada.BuscarCliente(datosCliente);
            
            if (clientes.Count == 0)
            {
                await ReplyAsync($"No se encontró ningún cliente");
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
                $"Se encontraron varios clientes.\n" +
                $"Elegí uno usando:\n`!elegirCliente <numero>`\n\n{listado}");
        }
    }
}