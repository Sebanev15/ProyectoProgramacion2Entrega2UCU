using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Interactions;
using Library;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;


public class CommandsEtiqueta: InteractionModuleBase<SocketInteractionContext>
{
    private readonly Fachada _fachada;

    public CommandsEtiqueta(Fachada fachada)
    {
        _fachada = fachada;
    }
    
    [SlashCommand("crearetiqueta", "Crea y agrega una etiqueta a un cliente.")]
    public async Task ExecuteAgregarEtiquetaAsync( [Remainder] string parametrosCliente, [Remainder] string etiqueta)
    {
        try
        {
            List<string> parametrosClientes = parametrosCliente.Split(' ').ToList();
            List<Cliente> clientes = _fachada.BuscarCliente(parametrosClientes);
            
            if (clientes.Count == 0 )
            {
                await ReplyAsync($"No se encontró ningún cliente con los parametros {parametrosCliente}.");
                return;
            }      
            if (clientes.Count == 1)
            {
                Cliente cliente = clientes.First();
                
                _fachada.CrearEtiqueta(cliente, etiqueta);
                
                await ReplyAsync($"Se creo la etiqueta {etiqueta} y se le asigno al usuario {cliente.Nombre}");
                return;
            }
            if (clientes.Count > 1)
            {
                SeleccionesUsuarios.OpcionesClientes[Context.User.Id] = clientes;
            
                var listado = string.Join("\n",
                    clientes.Select((c, i) => $"{i + 1}. {c.Nombre}"));

                await ReplyAsync(
                    $"Se encontraron varios admins con los parametros {parametrosCliente}.\n" +
                    $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
            }
        }
        catch (CampoInvalidoExepcion e)
        {
            await ReplyAsync(e.Message);
        }
    }    
}