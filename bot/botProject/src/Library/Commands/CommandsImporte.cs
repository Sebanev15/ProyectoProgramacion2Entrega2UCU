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
        
        if (Selecciones.ClienteSeleccionado.TryGetValue(Context.User.Id, out Cliente clienteSeleccionado))
        {
            
            _fachada.CrearCotizacion(fecha, monto, clienteSeleccionado);

            await ReplyAsync(
                $"Cotización creada para {clienteSeleccionado.Nombre}\n" +
                $"Monto: ${monto}\n" +
                $"Fecha: {fecha:dd-MM-yyyy}");
            
            Selecciones.ClienteSeleccionado.Remove(Context.User.Id);
            return;
        }   

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
        
        Selecciones.OpcionesClientes[Context.User.Id] = clientes;

        var listado = string.Join("\n",
            clientes.Select((c, i) => $"{i + 1}. Nombre: {c.Nombre} {c.Apellido} Telefono: {c.Telefono} Correo: {c.Correo} Genero: {c.Genero} Fecha de nacimiento: {c.FechaDeNacimiento}"));

        await ReplyAsync(
            $"Se encontraron varios clientes con los parametros {parametrosCliente}.\n" +
            $"Elegí uno usando:\n`/elegircliente <numero>`\n\n{listado}");
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
        
        if (Selecciones.ClienteSeleccionado.TryGetValue(Context.User.Id, out Cliente clienteSeleccionado))
        {
            
            _fachada.CrearVenta(producto,fecha, monto, clienteSeleccionado);

            await ReplyAsync(
                $"Venta creada para {clienteSeleccionado.Nombre}\n" +
                $"Monto: ${monto}\n" +
                $"Fecha: {fecha:dd-MM-yyyy}");                
            Selecciones.ClienteSeleccionado.Remove(Context.User.Id);
            return;
        }   
        
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
        
        Selecciones.OpcionesClientes[Context.User.Id] = clientes;

        var listado = string.Join("\n",
            clientes.Select((c, i) => $"{i + 1}. Nombre: {c.Nombre} {c.Apellido} Telefono: {c.Telefono} Correo: {c.Correo} Genero: {c.Genero} Fecha de nacimiento: {c.FechaDeNacimiento}"));

        await ReplyAsync(
            $"Se encontraron varios clientes con los parametros {parametrosCliente}.\n" +
            $"Elegí uno usando:\n`/elegircliente <numero>`\n\n{listado}");
    }
    
    [SlashCommand("modificarventa", "Modifica una venta")]
    public async Task ExecuteModificarVentaAsync([Remainder] string parametrosVentaVieja, [Remainder] string productoNuevo, double montoNuevo, DateTime fechaNueva, [Remainder] string parametrosClienteNuevo)
    {
        List<string> parametros = parametrosVentaVieja.Split(' ').ToList();
        List<string> parametrosCliente = parametrosClienteNuevo.Split(' ').ToList();
            
        List<Venta> ventas = _fachada.BuscarVentasSinFecha(parametros);
        List<Cliente> clientes = _fachada.BuscarCliente(parametrosCliente);
        
        bool hayClienteSeleccionado = Selecciones.ClienteSeleccionado.TryGetValue(Context.User.Id, out Cliente clienteSeleccionado);
        bool hayVentaSeleccionada = Selecciones.VentaSeleccionada.TryGetValue(Context.User.Id, out Venta ventaSeleccionada);

        if (hayClienteSeleccionado && hayVentaSeleccionada)
        {
            _fachada.ModificarImporte(
                ventaSeleccionada,
                new Venta(productoNuevo, fechaNueva, montoNuevo, clienteSeleccionado));

            await ReplyAsync("Se modificó la venta usando la venta y el cliente previamente seleccionados.");

            Selecciones.ClienteSeleccionado.Remove(Context.User.Id);
            Selecciones.VentaSeleccionada.Remove(Context.User.Id);
            return;
        }
        
        if (hayClienteSeleccionado)
        {

            Venta venta = ventas.First();
            _fachada.ModificarImporte(
                venta,
                new Venta(productoNuevo, fechaNueva, montoNuevo, clienteSeleccionado));

            await ReplyAsync("Se modificó la venta usando la venta encontrada y el cliente previamente seleccionado.");

            Selecciones.ClienteSeleccionado.Remove(Context.User.Id);
            return;
        }   
        
        if (hayVentaSeleccionada)
        {

            Cliente cliente = clientes.First();

            _fachada.ModificarImporte(
                ventaSeleccionada,
                new Venta(productoNuevo, fechaNueva, montoNuevo, cliente));

            await ReplyAsync("Se modificó la venta usando la venta seleccionada y el cliente encontrado por parámetros.");

            Selecciones.VentaSeleccionada.Remove(Context.User.Id);
            return;
        }

        if (ventas.Count == 0)
        {
            await ReplyAsync($"No se encontró ninguna venta con los parámetros {parametrosVentaVieja}.");
            return;
        }
            
        if (clientes.Count == 0)
        {
            await ReplyAsync($"No se encontró ningún cliente con los parámetros {parametrosClienteNuevo}.");
            return;
        }
        
        if (ventas.Count == 1 && clientes.Count == 1)
        { 
            Venta venta = ventas.First();
            Cliente cliente = clientes.First();
                
            _fachada.ModificarImporte(
                venta,
                new Venta(productoNuevo, fechaNueva, montoNuevo, cliente));

            await ReplyAsync("Se modificó la venta.");
            return;
        }
        
        if (ventas.Count > 1)
        {
            Selecciones.OpcionesVenta[Context.User.Id] = ventas;
                
            string listadoVentas = string.Join("\n",
                ventas.Select((v, i) => $"{i + 1}. Producto: {v.Producto} Fecha: {v.Fecha} Monto: {v.Monto} Cliente: {v.Cliente.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varias ventas con los parámetros {parametrosVentaVieja}.\n" +
                $"Elegí una usando:\n`/elegirventa <numero>`\n\n{listadoVentas}");
        }
        
        if (clientes.Count > 1)
        {
            Selecciones.OpcionesClientes[Context.User.Id] = clientes;
                
            string listadoClientes = string.Join("\n",
                clientes.Select((c, i) =>
                    $"{i + 1}. Nombre: {c.Nombre} {c.Apellido} Teléfono: {c.Telefono} Correo: {c.Correo} Género: {c.Genero} Nacimiento: {c.FechaDeNacimiento}"));

            await ReplyAsync(
                $"Se encontraron varios clientes con los parámetros {parametrosClienteNuevo}.\n" +
                $"Elegí uno usando:\n`/elegircliente <numero>`\n\n{listadoClientes}");
        }
    }
    
    [SlashCommand("agregarventa", "Agrega una venta ya existente a un cliente")]
    public async Task ExecuteAgregarVentaAsync([Remainder] string parametrosVenta, [Remainder] string parametrosCliente)
    {
        List<string> parametrosVentas = parametrosVenta.Split(' ').ToList();
        List<string> parametrosClientes = parametrosCliente.Split(' ').ToList();
        
        List<Venta> ventas = _fachada.BuscarVentasSinFecha(parametrosVentas);
        List<Cliente> clientes = _fachada.BuscarCliente(parametrosClientes);


        if (Selecciones.VentaSeleccionada.TryGetValue(Context.User.Id, out Venta ventaSel) &&
            Selecciones.ClienteSeleccionado.TryGetValue(Context.User.Id, out Cliente clienteSel))
        {
            _fachada.AgregarImporte(ventaSel, clienteSel);

            await ReplyAsync($"Se agregó la venta {ventaSel.Producto} al cliente {clienteSel.Nombre}.");

            Selecciones.VentaSeleccionada.Remove(Context.User.Id);
            Selecciones.ClienteSeleccionado.Remove(Context.User.Id);
            return;
        }
        
        if (Selecciones.ClienteSeleccionado.TryGetValue(Context.User.Id, out Cliente clienteSeleccionado))
        {
            Venta venta = ventas.First();
            _fachada.AgregarImporte(venta, clienteSeleccionado);

            await ReplyAsync($"Se agregó la venta {venta.Producto} al cliente {clienteSeleccionado.Nombre}.");
            Selecciones.ClienteSeleccionado.Remove(Context.User.Id);
            return;
        }   
        
        if (Selecciones.VentaSeleccionada.TryGetValue(Context.User.Id, out Venta ventaSeleccionada))
        {
            Cliente cliente = clientes.First();
            _fachada.AgregarImporte(ventaSeleccionada, cliente);

            await ReplyAsync($"Se agregó la venta {ventaSeleccionada.Producto} al cliente {cliente.Nombre}.");
            Selecciones.VentaSeleccionada.Remove(Context.User.Id);
            return;
        }
        
        if (ventas.Count == 0)
        {
            await ReplyAsync($"No se encontró ninguna venta con los parámetros {parametrosVenta}.");
            return;
        }
        
        if (clientes.Count == 0)
        {
            await ReplyAsync($"No se encontró ningún cliente con los parámetros {parametrosCliente}.");
            return;
        }
        
        if (ventas.Count == 1 && clientes.Count == 1)
        { 
            Venta venta = ventas.First();
            Cliente cliente = clientes.First();
            
            _fachada.AgregarImporte(venta, cliente);

            await ReplyAsync($"Se agregó la venta {venta.Producto} al cliente {cliente.Nombre}.");
            return;
        }
        
        if (ventas.Count > 1)
        {
            Selecciones.OpcionesVenta[Context.User.Id] = ventas;
            
            var listado = string.Join("\n",
                ventas.Select((v, i) => $"{i + 1}. Producto: {v.Producto} Fecha: {v.Fecha} Monto: {v.Monto} Cliente: {v.Cliente.Nombre}"));

            await ReplyAsync(
                $"Se encontraron varias ventas con los parámetros {parametrosVenta}.\n" +
                $"Elegí una usando:\n`/elegirventa <numero>`\n\n{listado}");
        }
        
        if (clientes.Count > 1)
        {
            Selecciones.OpcionesClientes[Context.User.Id] = clientes;
            
            var listado = string.Join("\n",
                clientes.Select((c, i) => $"{i + 1}. {c.Nombre} {c.Apellido} - {c.Correo} - {c.Telefono}"));

            await ReplyAsync(
                $"Se encontraron varios clientes con los parámetros {parametrosCliente}.\n" +
                $"Elegí uno usando:\n`/elegircliente <numero>`\n\n{listado}");
        }
    }
    
[SlashCommand("agregarcotizacion", "Agrega una cotización ya existente a un cliente")]
public async Task ExecuteAgregarCotizacionAsync(
    [Remainder] string parametrosCotizacion,
    [Remainder] string parametrosCliente)
{
    List<string> parametrosCotizaciones = parametrosCotizacion.Split(' ').ToList();
    List<string> parametrosClientes = parametrosCliente.Split(' ').ToList();

    List<Cotizacion> cotizaciones = _fachada.BuscarCotizacionessSinFecha(parametrosCotizaciones);
    List<Cliente> clientes = _fachada.BuscarCliente(parametrosClientes);
    

    if (Selecciones.CotizacionSeleccionada.TryGetValue(Context.User.Id, out Cotizacion cotSel)
        && Selecciones.ClienteSeleccionado.TryGetValue(Context.User.Id, out Cliente cliSel))
    {
        _fachada.AgregarImporte(cotSel, cliSel);

        await ReplyAsync($"Se agregó la cotización al cliente {cliSel.Nombre}");

        Selecciones.CotizacionSeleccionada.Remove(Context.User.Id);
        Selecciones.ClienteSeleccionado.Remove(Context.User.Id);
        return;
    }

    if (Selecciones.CotizacionSeleccionada.TryGetValue(Context.User.Id, out cotSel))
    {
        Cliente cliente = clientes.First();

        _fachada.AgregarImporte(cotSel, cliente);

        await ReplyAsync($"Se agregó la cotización al cliente {cliente.Nombre}");
        Selecciones.CotizacionSeleccionada.Remove(Context.User.Id);
        return;
    }


    if (Selecciones.ClienteSeleccionado.TryGetValue(Context.User.Id, out cliSel))
    {
        Cotizacion cot = cotizaciones.First();

        _fachada.AgregarImporte(cot, cliSel);

        await ReplyAsync($"Se agregó la cotización al cliente {cliSel.Nombre}");
        Selecciones.ClienteSeleccionado.Remove(Context.User.Id);
        return;
    }

    if (cotizaciones.Count == 0)
    {
        await ReplyAsync($"No se encontró ninguna cotización con los parámetros {parametrosCotizacion}.");
        return;
    }

    if (clientes.Count == 0)
    {
        await ReplyAsync($"No se encontró ningún cliente con los parámetros {parametrosCliente}.");
        return;
    }
    
    if (cotizaciones.Count == 1 && clientes.Count == 1)
    {
        Cotizacion cot = cotizaciones.First();
        Cliente cli = clientes.First();

        _fachada.AgregarImporte(cot, cli);
        await ReplyAsync($"Se agregó la cotización al cliente {cli.Nombre}");
        return;
    }
    
    if (cotizaciones.Count > 1)
    {
        Selecciones.OpcionesCotizacion[Context.User.Id] = cotizaciones;

        string listado = string.Join("\n",
            cotizaciones.Select((c, i) =>
                $"{i + 1}. Fecha: {c.Fecha:dd/MM/yyyy} Monto: {c.Monto} Cliente asociado: {c.Cliente?.Nombre ?? "Ninguno"}"));

        await ReplyAsync(
            $"Se encontraron varias cotizaciones con los parámetros {parametrosCotizacion}.\n" +
            $"Elegí una con:\n`/elegircotizacion <numero>`\n\n{listado}");
    }
    
    if (clientes.Count > 1)
    {
        Selecciones.OpcionesClientes[Context.User.Id] = clientes;

        string listado = string.Join("\n",
            clientes.Select((c, i) =>
                $"{i + 1}. {c.Nombre} {c.Apellido} | Tel: {c.Telefono} | Correo: {c.Correo}"));

        await ReplyAsync(
            $"Se encontraron varios clientes con los parámetros {parametrosCliente}.\n" +
            $"Elegí uno con:\n`/elegircliente <numero>`\n\n{listado}");
    }
}


    [SlashCommand("eliminarcotizacion", "Agrega una cotizacion ya existente a un cliente")]
    public async Task ExecuteeliminarCotizacionAsync([Remainder] string parametrosCotizacion)
    {
        List<string> parametrosCotizaciones = parametrosCotizacion.Split(' ').ToList();
        
        List<Cotizacion> cotizaciones = _fachada.BuscarCotizacionessSinFecha(parametrosCotizaciones);
        
        if (Selecciones.CotizacionSeleccionada.TryGetValue(Context.User.Id, out Cotizacion seleccionado))
        {
            _fachada.EliminarImporte(seleccionado);

            await ReplyAsync($"Se elimino la cotizacion");
            Selecciones.CotizacionSeleccionada.Remove(Context.User.Id);
            return;
        }
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
            Selecciones.OpcionesCotizacion[Context.User.Id] = cotizaciones;
            
            var listado = string.Join("\n",
                cotizaciones.Select((c, i) => $"{i + 1}. Cliente{c.Cliente} {c.Fecha} {c.Monto}"));

            await ReplyAsync(
                $"Se encontraron varias ventas con los parametros {parametrosCotizacion}.\n" +
                $"Elegí uno usando:\n`/elegircotizacion <numero>`\n\n{listado}");
        }
    }
    
    [SlashCommand("eliminarventa", "Agrega una venta ya existente a un cliente")]
    public async Task ExecuteEliminarVentaAsync([Remainder] string parametrosVenta)
    {
        List<string> parametrosVentas = parametrosVenta.Split(' ').ToList();
        
        List<Venta> ventas = _fachada.BuscarVentasSinFecha(parametrosVentas);
        
        if (Selecciones.VentaSeleccionada.TryGetValue(Context.User.Id, out Venta seleccionado))
        {
            _fachada.EliminarImporte(seleccionado);

            await ReplyAsync($"Se elimino la venta {seleccionado.Producto}");
            Selecciones.CotizacionSeleccionada.Remove(Context.User.Id);
            return;
        }
        
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
            Selecciones.OpcionesVenta[Context.User.Id] = ventas;
            
            var listado = string.Join("\n",
                ventas.Select((v, i) => $"{i + 1}. Producto: {v.Producto} Fecha: {v.Fecha} Monto: {v.Monto} Cliente asociado: {v.Cliente}"));

            await ReplyAsync(
                $"Se encontraron varias ventas con los parametros {parametrosVenta}.\n" +
                $"Elegí uno usando:\n`/elegirventa <numero>`\n\n{listado}");
        }
    }
}