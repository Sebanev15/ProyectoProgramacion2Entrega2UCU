using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Interactions;
using Library;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands;


public class CommandsDefensaAgus_VentasRangoPrecio: InteractionModuleBase<SocketInteractionContext>
{
    private readonly Fachada _fachada;

    public CommandsDefensaAgus_VentasRangoPrecio(Fachada fachada)
    {
        _fachada = fachada;
    }
    
    [SlashCommand("ObtenerVentasRangoPrecio", "Obtiene todos los clientes que contengan el Rango de Monto Indicado")]
    public async Task ExecuteObtenerVentasRangoPrecioAsync( [Remainder] string precioInicial, string precioFinal)
    {
        double precioInicialParse=double.Parse(precioInicial);
        double precioFinalParse=double.Parse(precioInicial);
        List<Cliente> clientes = _fachada.ObtenerVentasRangoPrecio(precioInicialParse, precioFinalParse);
        if (clientes.Count == 0)
        {
            await RespondAsync("No hay clientes que tengan ese monto de venta.");
            return;
        }

        var listaClientes = new StringBuilder();
        int i = 1;
        foreach (var cliente in clientes)
        {
            listaClientes.AppendLine($"{i}. {cliente.Nombre} {cliente.Apellido} - Tel: {cliente.Telefono} - Correo: {cliente.Correo} - Género: {cliente.Genero} - Fecha Nac: {cliente.FechaDeNacimiento:yyyy-MM-dd}");
            i++;
        }

        await RespondAsync(listaClientes.ToString());
    }
    [SlashCommand("ObtenerVentasNombreProducto", "Obtiene todos los clientes que contengan el Producto Indicado")]
    public async Task ExecuteObtenerVentasNombreProductoAsync( [Remainder] string nombreProducto)
    {
        List<Cliente> clientes = _fachada.ObtenerVentasProductoServicio(nombreProducto);
        if (clientes.Count == 0)
        {
            await RespondAsync("No hay clientes que tengan ese monto de venta.");
            return;
        }

        var listaClientes = new StringBuilder();
        int i = 1;
        foreach (var cliente in clientes)
        {
            listaClientes.AppendLine($"{i}. {cliente.Nombre} {cliente.Apellido} - Tel: {cliente.Telefono} - Correo: {cliente.Correo} - Género: {cliente.Genero} - Fecha Nac: {cliente.FechaDeNacimiento:yyyy-MM-dd}");
            i++;
        }

        await RespondAsync(listaClientes.ToString());
    }
    }    
