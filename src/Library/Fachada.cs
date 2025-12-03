using Library.interfaces;
using System;
using System.Collections.Generic;
using Ucu.Poo.DiscordBot.Domain;

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
    ///
    /// Aplica el Patrón de diseño Singleton:
    ///     Se asegura que solo haya una instancia de Fachada haciendo el constructor privado y creando un método
    ///     público de creación que chequee si ya hay una instancia [GetInstancia()].
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
            Cotizacion cotizacion = new Cotizacion(fecha, monto, cliente);
            _gestionCliente.AgregarImporte(cotizacion, cliente);
            return cotizacion;
        }
        
        public Venta CrearVenta(string producto, DateTime fecha, double monto, Cliente cliente)
        {
            Venta venta = new Venta(producto,fecha, monto, cliente);
            _gestionCliente.AgregarImporte(venta, cliente);
            return venta;
        }

        public Vendedor CrearVendedor(string nombre, string telefono, string correo)
        {
            GestionCliente gestionCliente = new GestionCliente();
            GestionUsuario gestionUsuario = new GestionUsuario();
            return new Vendedor(nombre, correo, telefono,gestionUsuario,gestionCliente);
        }        
        
        
        public Administrador CrearAdministrador(string nombre, string telefono, string correo)
        {
            GestionCliente gestionCliente = new GestionCliente();
            GestionUsuario gestionUsuario = new GestionUsuario();
            return new Administrador(nombre, correo, telefono,gestionUsuario,gestionCliente);
        }
        
        public Cliente CrearCliente(string nombre, string apellido, string telefono, string correo, string genero,
            DateTime fechaDeNacimiento)
        {
            return new Cliente(nombre, apellido, telefono, correo, genero, fechaDeNacimiento);
        }
        
        public void CrearEtiqueta(Cliente cliente, string nombreEtiqueta)
        {
             _gestionCliente.AgregarEtiqueta(cliente, new Etiqueta(nombreEtiqueta));    
        }

        public Mensaje CrearMensaje(DateTime fecha, string tema, Cliente cliente, Usuario usuario, bool esEnviado)
        {
            return new Mensaje(fecha, tema, cliente, usuario, esEnviado);
        }
        
        public Llamada CrearLlamada(DateTime fecha, string tema, Cliente cliente, Usuario usuario)
        {
            return new Llamada(fecha, tema, cliente, usuario);
        }
        
        public Reunion CrearReunion(DateTime fecha, string tema, Cliente cliente, Usuario usuario, string direccion)
        {
            return new Reunion(fecha, tema, cliente, usuario, direccion);
        }
        
        public Correo CrearCorreo(DateTime fecha, string tema, Cliente cliente, Usuario usuario, bool esEnviado)
        {
            return new Correo(fecha, tema, cliente, usuario, esEnviado);
        }
        
        // -------------------------------------- ETIQUETAS ------------------------------------------------------------
     
        
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

        public List<Cliente> BuscarCliente(List<string> datosBusqueda)
        {
            return _gestionCliente.BuscarCliente(datosBusqueda);
        }

        public void ListarClientes()
        {
            _gestionCliente.ListarClientes();
        }

        public List<Cliente> ListarClientesConReturn()
        {
           return _gestionCliente.ListarClientesConReturn();
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
        
        public List<IInteraccion> BuscarInteraccionesSinFecha(List<string> busqueda, Cliente cliente)
        {
            return _gestionCliente.BuscarInteraccionesSinFecha(busqueda, cliente);
        }

        public List<Venta> BuscarVentasSinFecha(List<string> datosBusqueda)
        {
            return _gestionCliente.BuscarVentasSinFecha(datosBusqueda);
        }        
        
        public List<Cotizacion> BuscarCotizacionessSinFecha(List<string> datosBusqueda)
        {
            return _gestionCliente.BuscarCotizacionesSinFecha(datosBusqueda);
        }

        public void AgregarComentarioInteraccion(IInteraccion interaccion, string comentario)
        {
            _gestionCliente.AgregarComentarioInteraccion(interaccion, comentario);
        }
        
        // ------------------------------------- ADMINISTRACIÓN --------------------------------------------------------
        public void ReactivarUsuario(Administrador admin, Usuario usuario)
        {
            _gestionUsuario.ReactivarUsuario(admin,usuario);
        }
        public void RegistrarUsuario(Administrador admin, Usuario usuario)
        {
            admin.RegistrarUsuario(usuario,admin.GestionUsuario);
        }

        public void SuspenderUsuario(Administrador admin, Usuario usuario)
        {
            _gestionUsuario.SuspenderUsuario(admin, usuario);
        }

        public void EliminarUsuario(Administrador admin, Usuario usuario)
        {
            admin.EliminarUsuario(usuario, _gestionUsuario);
        }
        
        public void AsignarOtroVendedor(Vendedor vendedorInicial, Vendedor vendedorAsignado, Cliente cliente)
        {
            _gestionUsuario.AsignarOtroVendedor(vendedorInicial, vendedorAsignado, cliente);
        }        
        
        public List<Usuario> BuscarUsuario(List<string> usuarioBusqueda)
        {
            return _gestionUsuario.BuscarUsuario(usuarioBusqueda);
        }
        
        //----------------------------------De aca para abajo es la Defensa---------------------------------------------

        public List<List<IImporte>> VentasConRango(int rangoMin, int rangoMax)
        {
            List<List<IImporte>> ventasConRango = _gestionCliente.VentasConRango(rangoMin, rangoMax);
            return ventasConRango;
        }

        public List<Cliente> ClientesConProductoDeterminado(string producto)
        {
            List<Cliente> clientesConProducto = _gestionCliente.ObtenerClientesConProducto(producto);
            return clientesConProducto;
        }
    }
}


///Historias de defensa
///
/// 1
/// Comando que retorne los clientes con ventas con un rango de monto
///
/// 2
/// Comando que retorne los clientes con ventas de cierto producto