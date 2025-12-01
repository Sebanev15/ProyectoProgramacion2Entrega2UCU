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
            Cliente cliente = clientes.First();

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
            Cliente cliente = clientes.First();

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
    
    [SlashCommand("modificarventa", "Modifica una venta")]
    public async Task ExecuteModificarVentaAsync([Remainder] string parametrosVentaVieja, [Remainder] string productoNuevo,double montoNuevo, DateTime fechaNueva, [Remainder] string parametrosClienteNuevo)
    {
        List<string> parametros = parametrosVentaVieja.Split(' ').ToList();
        List<string> parametrosCliente = parametrosClienteNuevo.Split(' ').ToList();
        List<Venta> ventas = _fachada.BuscarVentasSinFecha(parametros);
        List<Cliente> clientes = _fachada.BuscarCliente(parametrosCliente);
        
        if (ventas.Count == 0)
        {
            await ReplyAsync($"No se encontró ninguna venta con los parametros {parametrosVentaVieja}.");
            return;
        }
        
        if (clientes.Count == 0)
        {
            await ReplyAsync($"No se encontró ningún cliente con los parametros {parametrosClienteNuevo}.");
            return;
        }
        
        if (ventas.Count == 1 && clientes.Count == 1)
        { 
            Venta venta = ventas.First();
            Cliente cliente = clientes.First();
            
            _fachada.ModificarImporte(venta,new Venta(productoNuevo,fechaNueva,montoNuevo, cliente));

            await ReplyAsync("Se modifico la venta");
            return;
        }

        if (ventas.Count() > 1)
        {
            SeleccionesUsuarios.OpcionesVenta[Context.User.Id] = ventas;
            
            var listado = string.Join("\n",
                ventas.Select((v, i) => $"{i + 1}. {v.Producto}"));

            await ReplyAsync(
                $"Se encontraron varias ventas con los parametros {parametrosVentaVieja}.\n" +
                $"Elegí uno usando:\n`/elegirVenta <numero>`\n\n{listado}");
        }
        
        if (clientes.Count() > 1)
        {
            SeleccionesUsuarios.OpcionesClientes[Context.User.Id] = clientes;
            
            var listado = string.Join("\n",
                clientes.Select((c, i) => $"{i + 1}. {c.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varios clientes con los parametros {parametrosClienteNuevo}.\n" +
                $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
        }
    }
    
    [SlashCommand("agregarventa", "Agrega una venta ya existente a un cliente")]
    public async Task ExecuteAgregarVentaAsync([Remainder] string parametrosVenta, [Remainder] string parametrosCliente)
    {
        List<string> parametrosVentas = parametrosVenta.Split(' ').ToList();
        List<string> parametrosClientes = parametrosCliente.Split(' ').ToList();
        List<Venta> ventas = _fachada.BuscarVentasSinFecha(parametrosVentas);
        List<Cliente> clientes = _fachada.BuscarCliente(parametrosClientes);
        
        if (ventas.Count == 0)
        {
            await ReplyAsync($"No se encontró ninguna venta con los parametros {parametrosVenta}.");
            return;
        }
        
        if (clientes.Count == 0)
        {
            await ReplyAsync($"No se encontró ningún cliente con los parametros {parametrosCliente}.");
            return;
        }
        
        if (ventas.Count == 1 && clientes.Count == 1)
        { 
            Venta venta = ventas.First();
            Cliente cliente = clientes.First();
            
            _fachada.AgregarImporte(venta,cliente);

            await ReplyAsync($"Se agrego la venta {venta.Producto} al cliente {cliente.Nombre}");
            return;
        }

        if (ventas.Count() > 1)
        {
            SeleccionesUsuarios.OpcionesVenta[Context.User.Id] = ventas;
            
            var listado = string.Join("\n",
                ventas.Select((v, i) => $"{i + 1}. {v.Producto}"));

            await ReplyAsync(
                $"Se encontraron varias ventas con los parametros {parametrosVenta}.\n" +
                $"Elegí uno usando:\n`/elegirVenta <numero>`\n\n{listado}");
        }
        
        if (clientes.Count() > 1)
        {
            SeleccionesUsuarios.OpcionesClientes[Context.User.Id] = clientes;
            
            var listado = string.Join("\n",
                clientes.Select((c, i) => $"{i + 1}. {c.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varios clientes con los parametros {parametrosCliente}.\n" +
                $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
        }
    }
    
    [SlashCommand("agregarcotizacion", "Agrega una cotizacion ya existente a un cliente")]
    public async Task ExecuteAgregarCotizacionAsync([Remainder] string parametrosCotizacion, [Remainder] string parametrosCliente)
    {
        List<string> parametrosCotizaciones = parametrosCotizacion.Split(' ').ToList();
        List<string> parametrosClientes = parametrosCliente.Split(' ').ToList();
        List<Cotizacion> cotizaciones = _fachada.BuscarCotizacionessSinFecha(parametrosCotizaciones);
        List<Cliente> clientes = _fachada.BuscarCliente(parametrosClientes);
        
        if (cotizaciones.Count == 0)
        {
            await ReplyAsync($"No se encontró ninguna venta con los parametros {parametrosCotizacion}.");
            return;
        }
        
        if (clientes.Count == 0)
        {
            await ReplyAsync($"No se encontró ningún cliente con los parametros {parametrosCliente}.");
            return;
        }
        
        if (cotizaciones.Count == 1 && clientes.Count == 1)
        { 
            Cotizacion cotizacion = cotizaciones.First();
            Cliente cliente = clientes.First();
            
            _fachada.AgregarImporte(cotizacion,cliente);

            await ReplyAsync($"Se agrego la cotizacion al cliente {cliente.Nombre}");
            return;
        }

        if (cotizaciones.Count() > 1)
        {
            SeleccionesUsuarios.OpcionesCotizacion[Context.User.Id] = cotizaciones;
            
            var listado = string.Join("\n",
                cotizaciones.Select((c, i) => $"{i + 1}. Cliente{c.Cliente} {c.Fecha} {c.Monto}"));

            await ReplyAsync(
                $"Se encontraron varias ventas con los parametros {parametrosCotizacion}.\n" +
                $"Elegí uno usando:\n`/elegirCotizacion <numero>`\n\n{listado}");
        }
        
        if (clientes.Count() > 1)
        {
            SeleccionesUsuarios.OpcionesClientes[Context.User.Id] = clientes;
            
            var listado = string.Join("\n",
                clientes.Select((c, i) => $"{i + 1}. {c.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varios clientes con los parametros {parametrosCliente}.\n" +
                $"Elegí uno usando:\n`/elegirCliente <numero>`\n\n{listado}");
        }
    }

    [SlashCommand("eliminarcotizacion", "Agrega una cotizacion ya existente a un cliente")]
    public async Task ExecuteeliminarCotizacionAsync([Remainder] string parametrosCotizacion)
    {
        List<string> parametrosCotizaciones = parametrosCotizacion.Split(' ').ToList();
        List<Cotizacion> cotizaciones = _fachada.BuscarCotizacionessSinFecha(parametrosCotizaciones);
        
        if (cotizaciones.Count == 0)
        {
            await ReplyAsync($"No se encontró ninguna venta con los parametros {parametrosCotizacion}.");
            return;
        }
        
        if (cotizaciones.Count == 1)
        { 
            Cotizacion cotizacion = cotizaciones.First();
            
            _fachada.EliminarImporte(cotizacion);

            await ReplyAsync($"Se elimino la cotizacion");
            return;
        }

        if (cotizaciones.Count() > 1)
        {
            SeleccionesUsuarios.OpcionesCotizacion[Context.User.Id] = cotizaciones;
            
            var listado = string.Join("\n",
                cotizaciones.Select((c, i) => $"{i + 1}. Cliente{c.Cliente} {c.Fecha} {c.Monto}"));

            await ReplyAsync(
                $"Se encontraron varias ventas con los parametros {parametrosCotizacion}.\n" +
                $"Elegí uno usando:\n`/elegirCotizacion <numero>`\n\n{listado}");
        }
    }
    
    [SlashCommand("eliminarventa", "Agrega una venta ya existente a un cliente")]
    public async Task ExecuteEliminarVentaAsync([Remainder] string parametrosVenta)
    {
        List<string> parametrosVentas = parametrosVenta.Split(' ').ToList();
        List<Venta> ventas = _fachada.BuscarVentasSinFecha(parametrosVentas);
        
        if (ventas.Count == 0)
        {
            await ReplyAsync($"No se encontró ninguna venta con los parametros {parametrosVenta}.");
            return;
        }
        
        if (ventas.Count == 1)
        { 
            Venta venta = ventas.First();
            
            _fachada.EliminarImporte(venta);

            await ReplyAsync($"Se elimino la venta {venta.Producto}");
            return;
        }

        if (ventas.Count() > 1)
        {
            SeleccionesUsuarios.OpcionesVenta[Context.User.Id] = ventas;
            
            var listado = string.Join("\n",
                ventas.Select((v, i) => $"{i + 1}. {v.Producto}"));

            await ReplyAsync(
                $"Se encontraron varias ventas con los parametros {parametrosVenta}.\n" +
                $"Elegí uno usando:\n`/elegirVenta <numero>`\n\n{listado}");
        }
    }
}