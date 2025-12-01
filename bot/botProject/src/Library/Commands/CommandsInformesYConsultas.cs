using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Interactions;
using Library;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;


public class CommandsInformesYConsultas: InteractionModuleBase<SocketInteractionContext>
{
    private readonly Fachada _fachada;

    public CommandsInformesYConsultas(Fachada fachada)
    {
        _fachada = fachada;
    }

    
    [SlashCommand("obtenerclientesinactivos", "Devuelve una lista de los clientes inactivos")]
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

    [SlashCommand("obtenerclientesnorespondidos", "Devuelve una lista de los clientes sin responder")]
    public async Task ExecuteAsync2()
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

    [SlashCommand("obtenerventastotales", "Devuelve todas las ventas en un periodo de tiempo")]
    public async Task ExecuteAsync3(DateTime fechaInicio, DateTime fechaFin)
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