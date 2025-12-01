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
    public async Task ExecuteCrearEtiquetaAsync( [Remainder] string parametrosCliente, [Remainder] string etiqueta)
    {
        try
        {
            List<string> parametrosClientes = parametrosCliente.Split(' ').ToList();
            
            List<Cliente> clientes = _fachada.BuscarCliente(parametrosClientes);
            
            if (Selecciones.ClienteSeleccionado.TryGetValue(Context.User.Id, out Cliente seleccionado))
            {
                _fachada.CrearEtiqueta(seleccionado, etiqueta);

                await ReplyAsync($"Se creó la etiqueta {etiqueta} para {seleccionado.Nombre}");
                Selecciones.ClienteSeleccionado.Remove(Context.User.Id);
                return;
            }
            else if (clientes.Count == 0 )
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
                Selecciones.OpcionesClientes[Context.User.Id] = clientes;
            
                var listado = string.Join("\n",
                    clientes.Select((c, i) => $"{i + 1}. Nombre: {c.Nombre} {c.Apellido} Telefono: {c.Telefono} Correo: {c.Correo} Genero: {c.Genero} Fecha de nacimiento: {c.FechaDeNacimiento}"));

                await ReplyAsync(
                    $"Se encontraron varios clientes con los parametros {parametrosCliente}.\n" +
                    $"Elegí uno usando:\n`/elegircliente <numero>`\n\n{listado}");

            }
        }
        catch (CampoInvalidoExepcion e)
        {
            await ReplyAsync(e.Message);
        }
    }    
}