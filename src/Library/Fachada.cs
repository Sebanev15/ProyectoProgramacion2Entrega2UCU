using Library.abstractions;
using Library.interfaces;
using System;
using System.Collections.Generic;

namespace Library
{
    public class Fachada
{
    private GestionSistema _gestionSistema;
    public Fachada(GestionSistema gestionSistema)
    {
        _gestionSistema = gestionSistema;
    }
    
    public Cotizacion CrearCotizacion(DateTime fecha, double monto, Cliente cliente)
    {
        return new Cotizacion(fecha, monto, cliente);
    }
    
    public Venta CrearVenta(string producto, DateTime fecha, double monto, Cliente cliente)
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

    public void AgregarCliente(Cliente cliente){
        _gestionSistema.AgregarCliente(cliente);
    }
    
    public void ModificarCliente (Cliente clienteBase, Cliente clienteModificado)
    {
        _gestionSistema.ModificarCliente(clienteBase, clienteModificado);
    }

    public void EliminarCliente(Cliente cliente)
    {
        _gestionSistema.EliminarCliente(cliente);
    }

    public List<Cliente> BuscarCliente(string clienteBusqueda)
    {
        return _gestionSistema.BuscarCliente(clienteBusqueda);
    }

    public void ListarClientes()
    {
        _gestionSistema.ListarClientes();
    }

    public void AgregarEtiqueta(Cliente cliente, Etiqueta etiqueta)
    {
        _gestionSistema.AgregarEtiqueta(cliente, etiqueta);
    }

    public List<Cliente> ObtenerClientesInactivos()
    {
        return _gestionSistema.ObtenerClientesInactivos();
    }

    public List<Cliente> ObtenerClientesNoRespondidos()
    {
        return _gestionSistema.ObtenerClientesNoRespondidos();
    }

    public List<IImporte> ObtenerVentasTotales(DateTime fechaInicio, DateTime fechaFin)
    {
        return _gestionSistema.ObtenerVentasTotales(fechaInicio, fechaFin);
    }

    public void AgregarImporte(IImporte importe, Cliente cliente)
    {
        _gestionSistema.AgregarImporte(importe, cliente);
    }

    public void RegistrarInteraccion(Cliente cliente, IInteraccion interaccion)
    {
        _gestionSistema.RegistrarInteraccion(cliente, interaccion);
    }

    public List<IInteraccion> BuscarInteracciones(DateTime fecha, string busqueda)
    {
        return _gestionSistema.BuscarInteracciones(fecha, busqueda);
    }

    public void AgregarComentario(IInteraccion interaccion, string comentario)
    {
        _gestionSistema.AgregarComentario(interaccion, comentario);
    }
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
        admin.EliminarUsuario(usuario, sistema);
    }

    public void AsignarOtroVendedor(Vendedor vendedorInicial, Vendedor vendedorAsignado, Cliente cliente)
    {
        vendedorInicial.AsignarOtroVendedor(vendedorAsignado, cliente);
    }
}
}
