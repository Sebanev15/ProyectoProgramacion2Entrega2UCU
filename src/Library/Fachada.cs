using Library.abstractions;
using Library.interfaces;
using System;
using System.Collections.Generic;

namespace Library
{    
    /// <summary>
    /// La clase actúa como una interfaz unificada que guia las operaciones principales del sistema.
    /// </summary>
    /// <remarks>
    /// Aplica Bajo Acoplamiento:
    ///     Evita que otras capas dependan directamente de GestionSistema, manteniendo bajo acoplamiento.
    ///
    /// Aplica Alta Cohesion:
    ///     Centraliza las operaciones del sistema en un solo punto de acceso con una responsabilidad clara.
    ///
    /// Aplica DIP (Dependency Inversion Principle):
    ///     Las capas externas dependen de esta abstracción de alto nivel (la fachada),
    ///     no de las implementaciones concretas.
    ///
    /// Aplica el Patrón de diseño Fachada(lo es):
    ///     Simplifica el uso del sistema al ofrecer una interfaz única y coherente para múltiples clases internas.
    /// </remarks>
    
    public class Fachada
    {
        private IGestionCliente _gestionCliente { get; }
        private IGestionUsuario _gestionUsuario { get; }
        
        private static Fachada _instance;
        private Fachada()
        {
            _gestionCliente = new GestionCliente();
            _gestionUsuario = new GestionUsuario();
        }
        
        public static Fachada GetInstancia()
        {
            if (_instance == null)
            {
                _instance = new Fachada();
            }
            return _instance;
        }

        public IGestionCliente GetGestionCliente()
        {
            return _gestionCliente;
        }
        
        public IGestionUsuario GetGestionUsuario()
        {
            return _gestionUsuario;
        }
        // -------------------------------------- CREACIÓN DE ENTIDADES ------------------------------------------------
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
        
        // -------------------------------------- ETIQUETAS ------------------------------------------------------------
        public void AgregarEtiqueta(Etiqueta etiqueta, Cliente cliente)
        {
            _gestionCliente.AgregarEtiqueta(cliente, etiqueta);
        }
        
        // -------------------------------------- GESTIÓN DE CLIENTES --------------------------------------------------
        public void AgregarCliente(Cliente cliente){
            _gestionCliente.AgregarCliente(cliente);
        }
        
        public void ModificarCliente (Cliente clienteBase, Cliente clienteModificado)
        {
            _gestionCliente.ModificarCliente(clienteBase, clienteModificado);
        }

        public void EliminarCliente(Cliente cliente)
        {
            _gestionCliente.EliminarCliente(cliente);
        }

        public List<Cliente> BuscarCliente(string clienteBusqueda)
        {
            return _gestionCliente.BuscarCliente(clienteBusqueda);
        }

        public void ListarClientes()
        {
            _gestionCliente.ListarClientes();
        }
        
        
        // -------------------------------------- INFORMES Y CONSULTAS -------------------------------------------------
        public List<Cliente> ObtenerClientesInactivos()
        {
            return _gestionCliente.ObtenerClientesInactivos();
        }

        public List<Cliente> ObtenerClientesNoRespondidos()
        {
            return _gestionCliente.ObtenerClientesNoRespondidos();
        }

        public List<string> ObtenerVentasTotales(DateTime fechaInicio, DateTime fechaFin)
        {
            return _gestionCliente.ObtenerVentasTotales(fechaInicio, fechaFin);
        }
        
        // -------------------------------------- IMPORTES E INTERACCIONES ---------------------------------------------
        public void AgregarImporte(IImporte importe, Cliente cliente)
        {
            _gestionCliente.AgregarImporte(importe, cliente);
        }

        public void ModificarImporte(IImporte importeBase, IImporte importeModificar)
        {
            _gestionCliente.ModificarImporte(importeBase, importeModificar);
        }

        public void EliminarImporte(IImporte importe)
        {
            _gestionCliente.EliminarImporte(importe);
        }

        public void RegistrarInteraccion(Cliente cliente, IInteraccion interaccion)
        {
            _gestionCliente.RegistrarInteraccion(cliente, interaccion);
        }

        public List<IInteraccion> BuscarInteracciones(DateTime fecha, string busqueda, Cliente cliente)
        {
            return _gestionCliente.BuscarInteracciones(fecha, busqueda, cliente);
        }

        public void AgregarComentarioInteraccion(IInteraccion interaccion, string comentario)
        {
            _gestionCliente.AgregarComentarioInteraccion(interaccion, comentario);
        }
        
        // ------------------------------------- ADMINISTRACIÓN --------------------------------------------------------
        public void AdminReactivarUsuario(Administrador admin, Usuario usuario)
        {
            _gestionUsuario.CrearUsuario(admin,usuario);
        }
        public void AdminCrearUsuario(Administrador admin, Usuario usuario, GestionUsuario gestionUsuario)
        {
            admin.CrearUsuario(usuario,gestionUsuario);
        }

        public void AdminSuspenderUsuario(Administrador admin, Usuario usuario)
        {
            _gestionUsuario.ReactivarUsuario(admin, usuario);
        }

        public void AdminEliminarUsuario(Administrador admin, Usuario usuario, GestionUsuario gestionUsuario)
        {
            admin.EliminarUsuario(usuario, gestionUsuario);
        }
        
        public void AsignarOtroVendedor(Vendedor vendedorInicial, Vendedor vendedorAsignado, Cliente cliente)
        {
            _gestionUsuario.AsignarOtroVendedor(vendedorInicial, vendedorAsignado, cliente);
        }
    }
}
