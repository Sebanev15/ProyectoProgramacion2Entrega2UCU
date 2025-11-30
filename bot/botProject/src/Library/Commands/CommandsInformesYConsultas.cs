using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;

public class CommandsInformesYConsultas
{
    public class CommandObtenerClientesInactivos : ModuleBase<SocketCommandContext>
    {
        private readonly Fachada _fachada;

        public CommandObtenerClientesInactivos(Fachada fachada)
        {
            _fachada = fachada;
        }

        [Command("obtenerClientesInactivos")]
        public async Task ExecuteAsync()
        {
            try
            {
                List<Cliente> resultado = _fachada.ObtenerClientesInactivos();

                string mensaje = "Clientes inactivos:\n" + string.Join("\n", resultado.Select(c => $"- {c.Nombre} {c.Apellido}"));

                await ReplyAsync(mensaje);
            }
            catch (ListaVaciaExcepcion e)
            {
                await ReplyAsync(e.Message);
            }
        }
    }
    
    public class CommandObtenerClientesNoRespondidos : ModuleBase<SocketCommandContext>
    {
        private readonly Fachada _fachada;

        public CommandObtenerClientesNoRespondidos(Fachada fachada)
        {
            _fachada = fachada;
        }

        [Command("obtenerClientesNoRespondidos")]
        public async Task ExecuteAsync()
        {
            try
            {
                List<Cliente> resultado = _fachada.ObtenerClientesNoRespondidos();

                string mensaje = "Clientes no respondidos:\n" + string.Join("\n", resultado.Select(c => $"- {c.Nombre} {c.Apellido}"));

                await ReplyAsync(mensaje);
            }
            catch (ListaVaciaExcepcion e)
            {
                await ReplyAsync(e.Message);
            }
        }
    }
    
    public class CommandObtenerVentasTotales : ModuleBase<SocketCommandContext>
    {
        private readonly Fachada _fachada;

        public CommandObtenerVentasTotales(Fachada fachada)
        {
            _fachada = fachada;
        }

        [Command("obtenerVentasTotales")]
        public async Task ExecuteAsync(DateTime fechaInicio, DateTime fechaFin)
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
}