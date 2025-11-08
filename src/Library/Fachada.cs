using Library.abstractions;
using Library.interfaces;
using System;
using System.Collections.Generic;

namespace Library
{
    public class Fachada
{
    private GestionImporteBase _gestionImporte;
    private GestionInteraccionBase _gestionInteraccion;
    private GestionClienteBase _gestionCliente;
    public Fachada(
        GestionImporteBase gestionImporte,
        GestionInteraccionBase gestionInteraccion,
        GestionClienteBase gestionCliente)
    {
        _gestionImporte = gestionImporte;
        _gestionInteraccion = gestionInteraccion;
        _gestionCliente = gestionCliente;
    }
    
    public Cotizacion CrearCotizacion(DateTime fecha, double monto, IClienteBase cliente)
    {
        return new Cotizacion(fecha, monto, cliente);
    }
    
    public Venta CrearVenta(string producto, DateTime fecha, double monto, IClienteBase cliente)
    {
        return new Venta(producto,fecha, monto, cliente);
    }
    
    public Cliente CrearCliente(string nombre, string apellido, string telefono, string correo, string genero,
        DateTime fechaDeNacimiento)
    {
        return new Cliente(nombre, apellido, telefono, correo, genero, fechaDeNacimiento);
    }

    public Etiqueta CrearEtiqueta(string nombreEtiqueta)
    {
        return new Etiqueta(nombreEtiqueta);
    }

    public void AgregarCliente(IClienteBase cliente){
        _gestionCliente.AgregarCliente(cliente);
    }
    
    public void ModificarCliente (IClienteBase clienteBase, IClienteBase clienteModificado)
    {
        _gestionCliente.ModificarCliente(clienteBase, clienteModificado);
    }

    public void EliminarCliente(IClienteBase cliente)
    {
        _gestionCliente.EliminarCliente(cliente);
    }

    public List<IClienteBase> BuscarCliente(string clienteBusqueda)
    {
        return _gestionCliente.BuscarCliente(clienteBusqueda);
    }

    public void ListarClientes()
    {
        _gestionCliente.ListarClientes();
    }

    public void AgregarEtiqueta(IClienteBase cliente, Etiqueta etiqueta)
    {
        _gestionCliente.AgregarEtiqueta(cliente, etiqueta);
    }

    public void ObtenerClientesInactivos()
    {
        _gestionCliente.ObtenerClientesInactivos();
    }

    public void ObtenerClientesNoRespondidos()
    {
        _gestionCliente.ObtenerClientesNoRespondidos();
    }

    public void ObtenerVentasTotales(DateTime fechaInicio, DateTime fechaFin)
    {
        _gestionImporte.ObtenerVentasTotales(fechaInicio, fechaFin);
    }

    public void AgregarImporte(IImporte importe, IClienteBase cliente)
    {
        _gestionImporte.AgregarImporte(importe, cliente);
    }

    public void RegistrarInteraccion(IClienteBase cliente, IInteraccion interaccion)
    {
        _gestionInteraccion.RegistrarInteraccion(cliente, interaccion);
    }

    public void BuscarInteracciones(DateTime fecha, string busqueda)
    {
        _gestionInteraccion.BuscarInteracciones(fecha, busqueda);
    }

    public void AgregarComentario(IInteraccion interaccion, string comentario)
    {
        _gestionInteraccion.AgregarComentario(interaccion, comentario);
    }
    //UsuarioBase
    public static void AdminReactivarUsuario(Administrador admin, UsuarioBase usuario)
    {
        admin.ReactivarUsuario(usuario);
    }
    public void AdminCrearUsuario(Administrador admin, UsuarioBase usuario, System sistema)
    {
        admin.CrearUsuario(usuario,sistema);
    }

    public static void AdminSuspenderUsuario(Administrador admin, UsuarioBase usuario)
    {
        admin.SuspenderUsuario(usuario);
    }

    public void AdminEliminarUsuario(Administrador admin, UsuarioBase usuario, System sistema)
    {
        admin.EliminarUsuario(usuario,sistema);
    }
    //Vendedor

    public void AsignarOtroVendedor(Vendedor vendedorInicial, Vendedor vendedorAsignado, IClienteBase cliente)
    {
        vendedorInicial.AsignarOtroVendedor(vendedorAsignado, cliente);
    }
}
}
