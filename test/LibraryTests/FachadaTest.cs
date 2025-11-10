using Library;
using NUnit.Framework;
using Library.abstractions;
using Library.interfaces;
using System.Collections.Generic;
using System;
using System.IO;

namespace LibraryTest
{
    [TestFixture]
    public class FachadaTest
    {
        private GestionSistema _gestionSistema;
        private Fachada _fachada;
        private DateTime _fecha;
        private Cliente _cliente;
        private Usuario _usuario;
        private IInteraccion _interaccion;
        private Administrador _admin;
        private Etiqueta _etiqueta;
        [SetUp]
        public void Setup()
        {
            _gestionSistema = new GestionSistema();
            _fecha = new DateTime(2024, 10, 1);
            _cliente = new Cliente("juan", "smith", "12345678", "juansmith007@gmail.com", "M",_fecha);
            _fachada = new Fachada(_gestionSistema);
            _usuario = new Usuario("Usuariooo", "usuario@mail.com", "23423423", new GestionSistema());
            _interaccion = new Correo(_fecha, "importante", _cliente, _usuario, true);
            _admin = new Administrador("Mauro", "mauroeladmin@gmail.com", "12341234", new GestionSistema());
            _etiqueta = new Etiqueta("Importante");
        }

        [Test]
        public void CrearCotizacionFachadaTest()
        {
            var fecha = _fecha;
            var cliente = _cliente;
            var monto = 1200;

            var cotizacion = _fachada.CrearCotizacion(fecha, monto, cliente);

            Assert.That(cotizacion, Is.Not.Null);
            Assert.That(cotizacion.Fecha, Is.EqualTo(fecha));
            Assert.That(cotizacion.Monto, Is.EqualTo(monto));
            Assert.That(cotizacion.Cliente, Is.EqualTo(cliente));
        }

        [Test]
        public void CrearVentaFachadaTest()
        {
            var venta = _fachada.CrearVenta("procesador", _fecha, 20000, _cliente);
            Assert.That(venta, Is.Not.Null);
            Assert.That(venta.Fecha, Is.EqualTo(_fecha));
            Assert.That(venta.Monto, Is.EqualTo(20000));
            Assert.That(venta.Cliente, Is.EqualTo(_cliente));
        }

        [Test]
        public void CrearClienteFachadaTest()
        {
            var cliente2 = _fachada.CrearCliente("Ana", "García", "12345678", "ana@mail.com", "F", new DateTime(1995, 5, 10));

            Assert.That(cliente2.Nombre, Is.EqualTo("Ana"));
            Assert.That(cliente2.Apellido, Is.EqualTo("García"));
            Assert.That(cliente2.Telefono, Is.EqualTo("12345678"));
            Assert.That(cliente2.Correo, Is.EqualTo("ana@mail.com"));
            Assert.That(cliente2.Genero, Is.EqualTo("F"));
            Assert.That(cliente2.FechaDeNacimiento, Is.EqualTo(new DateTime(1995, 5, 10)));
        }

        [Test]
        public void CrearEtiquetaFachadaTest()
        {
            var etiqueta = _fachada.CrearEtiqueta("rimbombante");
            Assert.That(etiqueta.NombreEtiqueta, Is.EqualTo("rimbombante"));

        }
        

        [Test]
        public void ModificarClienteFachadaTest()
        {
            var fecha = new DateTime(2024, 10, 1);
            var original = new Cliente("juan", "smith", "12345678", "juansmith007@gmail.com", "M",fecha);
            var modificado = new Cliente("juan2", "smith2", "123456789", "juansmith008@gmail.com", "M",fecha);
            var etiqueta = _etiqueta;
            var etiqueta2 = new Etiqueta("MUY importante");
            _fachada.AgregarCliente(original);
            _fachada.AgregarCliente(modificado);
            _fachada.AgregarEtiqueta(original,etiqueta);
            _fachada.AgregarEtiqueta(modificado,etiqueta2);
            _fachada.ModificarCliente(original, modificado);

            Assert.That(original.Nombre, Is.EqualTo(modificado.Nombre));
            Assert.That(original.Etiquetas.Count, Is.EqualTo(1));
            Assert.That(original.Etiquetas[0]!=modificado.Etiquetas[0]);

        }

        [Test]
        public void AgregarEliminarClienteFachadaTest()
        {
            var fecha = _fecha;
            var cliente = _cliente;
            _fachada.AgregarCliente(cliente);
            Assert.That(_gestionSistema.Clientes, Does.Contain(cliente));
            _fachada.EliminarCliente(cliente);
            Assert.That(_gestionSistema.Clientes, Does.Not.Contain(cliente));
        }

        [Test]
        public void BuscarClienteFachadaTest()
        {
            var resultado = _fachada.BuscarCliente("Pedro");
            Assert.That(resultado, Is.EqualTo(_gestionSistema.BuscarCliente("Pedro")));
        }

        [Test]
        public void ListarClientesFachadaTest()
        {
            DateTime fechaN = new DateTime(2024, 10, 20);
            var jorjito = new Cliente("jorjito", "perez", "00", "monson@gmail.com", "M", fechaN);
            var jorge = new Cliente("jorge", "perez", "00", "monson@gmail.com", "M", fechaN);
            
            var gestionSist = _gestionSistema;
            gestionSist.AgregarCliente(jorge);
            gestionSist.AgregarCliente(jorjito);
            Assert.That(gestionSist.Clientes.Count, Is.EqualTo(2));
            var sw = new StringWriter();
            Console.SetOut(sw);
            _fachada.ListarClientes();
            string resultado = sw.ToString();
            Assert.That(resultado, Does.Contain("jorjito"));
            Assert.That(resultado, Does.Contain("jorge"));
        }
        [Test]
        public void AgregarEtiquetaFachadaTest()
        {
            var cliente = _cliente;
            var etiqueta = new Etiqueta("Premium"); GestionSistema gestionCliente = new GestionSistema();

            _fachada.AgregarEtiqueta(cliente, etiqueta);

            Assert.That(cliente.Etiquetas.Contains(etiqueta));
        }

        [Test]
        public void ObtenerClientesInactivosFachadaTest()
        {
            var gestionSist = _gestionSistema;
            var cliente = _cliente;
            DateTime fechaNueva = new DateTime(2024, 10, 20);
            cliente.Interacciones.Add(new Reunion(fechaNueva, "Reunion1", cliente, _usuario, "Eiffel" ));
            Assert.That(_fachada.ObtenerClientesInactivos(),Is.EqualTo(gestionSist.ObtenerClientesInactivos()));
        }

        [Test]
        public void ObtenerClientesNoRespondidosTest()
        {
            var usuario = _usuario;
            DateTime fechaN = new DateTime(2024, 10, 20);
            var jorjito = new Cliente("jorjito", "perez", "00", "monson@gmail.com", "M", fechaN);
            var jorge = new Cliente("jorge", "perez", "00", "monson@gmail.com", "M", fechaN);
            _gestionSistema.AgregarCliente(jorge);
            _gestionSistema.AgregarCliente(jorjito);
            Assert.That(_gestionSistema.Clientes.Count, Is.EqualTo(2));
            DateTime fechaNueva = new DateTime(2024, 10, 20);
            Reunion reunion = new Reunion(fechaNueva, "Reunion1", jorge, usuario, "Eiffel");
            jorge.Interacciones.Add(reunion);
             
            List<Cliente> resultado= _gestionSistema.ObtenerClientesNoRespondidos();
            Assert.That(resultado.Count, Is.EqualTo(1));
            Assert.That(_fachada.ObtenerClientesNoRespondidos(),Is.EqualTo(_gestionSistema.ObtenerClientesNoRespondidos()));
             
            string comentario = "Esta reunion fue respondida";
            reunion.Comentarios.Add(comentario);
            resultado = _gestionSistema.ObtenerClientesNoRespondidos();
            Assert.That(resultado.Count, Is.EqualTo(0));
            Assert.That(_fachada.ObtenerClientesNoRespondidos(),Is.EqualTo(_gestionSistema.ObtenerClientesNoRespondidos()));

        }
        
        [Test]
        public void ObtenerVentasTotalesTest()
        {
            var gestionSist = _gestionSistema;
            var fecha = new DateTime(2024, 10, 20);
            var fecha1 = new DateTime(2024, 11, 20);
            var fecha2 = new DateTime(2024, 12, 20);
            var venta = new Venta("caja", fecha, 12, _cliente);
            var venta1 = new Cotizacion(fecha1, 12, _cliente);
            gestionSist.Importes = new List<IImporte>();
            gestionSist.Importes.Add(venta);
            
            List<IImporte> resultado = gestionSist.ObtenerVentasTotales(fecha,fecha2);

            Assert.That(resultado.Count, Is.EqualTo(1));
            Assert.That(_fachada.ObtenerVentasTotales(fecha,fecha2),Is.EqualTo(gestionSist.ObtenerVentasTotales(fecha,fecha2)));
        }
        
        [Test]
        public void AgregarImporteFachadaTest()
        {
            var fecha = _fecha;
            var cliente = _cliente;
            var importe = _fachada.CrearCotizacion(fecha, 2000, cliente);
            _fachada.AgregarImporte(importe,_cliente);
        }
        
        [Test]
        public void RegistrarInteraccionFachadaTest()
        {
            var cliente = _cliente;
            var interaccion = _interaccion;

            _fachada.RegistrarInteraccion(cliente, interaccion);

            Assert.That(cliente.Interacciones, Does.Contain(interaccion));
        }
        
        [Test]
        public void BuscarInteraccionesTest()
        {
            var interaccion = _interaccion;
            interaccion.Comentarios = new List<string>();
            interaccion.Comentarios.Add("hola");
            _gestionSistema.Interacciones.Add(interaccion);
            List<IInteraccion> resultado= _fachada.BuscarInteracciones(_fecha, "importante");
            Assert.That(resultado.Count, Is.EqualTo(1));
        }

        [Test]
        public void AgregarComentarioTesting()
        {
            var interaccion = _interaccion;
            interaccion.Comentarios = new List<string>();
            _fachada.AgregarComentario(interaccion, "Finalizada en 10m");
            Assert.That(interaccion.Comentarios.Contains("Finalizada en 10m"), Is.True);
        }

        [Test]
        public void SuspenderReactivarUsuarioFachadaTest()
        {
            var usuario = _usuario;
            var admin = _admin;
            
            Fachada.AdminSuspenderUsuario(admin,usuario);
            Assert.That(usuario.EstaSuspendido, Is.True);
            Fachada.AdminReactivarUsuario(admin,usuario);
            Assert.That(usuario.EstaSuspendido, Is.False);
        }
        
        [Test]
        public void CrearUsuarioTest()
        {
            var administrador = _admin;
            var sistema = new Library.System();
            var usuarioGenerico1 = new Usuario("NombreGenerico", "correo@gmail.com", "099222333", _gestionSistema);
            var usuarioGenerico2 = new Usuario("NombreGenerico", "correo2@gmail.com", "099333444", _gestionSistema);
            sistema.usuarios.Add(usuarioGenerico1);
            administrador.CrearUsuario(usuarioGenerico2, sistema);
            Assert.That(sistema.usuarios.Count, Is.EqualTo(2));
            Assert.That(sistema.usuarios[1], Is.EqualTo(usuarioGenerico2));
        }

        [Test]
        public void CrearUsuarioYaExistenteTest()
        {
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
            var administrador = _admin;
            var sistema = new Library.System();
            var usuarioGenerico1 = new Usuario("NombreGenerico", "correo@gmail.com", "099222333",_gestionSistema);
            
            administrador.CrearUsuario(usuarioGenerico1, sistema);
            string output = consoleOutput.ToString();
            Assert.That(output.Contains("ERROR: No se pudo añadir el usuario al sistema"));
        }
        
        [Test]
        public void EliminarUsuarioTest()
        {
            var administrador = _admin;
            var sistema = new Library.System();
            var usuarioGenerico1 = new Usuario("NombreGenerico", "correo@gmail.com", "099222333",_gestionSistema);
            
            administrador.EliminarUsuario(usuarioGenerico1, sistema);
            Assert.That(sistema.usuarios.Count, Is.EqualTo(0));
        }

        [Test]
        public void AsignarOtroVendedorCorrectoTest()
        {
            var vendedor1 = new Vendedor("juan", "juan@gmail.com", "099222333",_gestionSistema);
            var vendedor2 = new Vendedor("juan2", "juan@gmail.com", "099222333",_gestionSistema);
            
            var cliente = new Cliente("Pepe", "Rodriguez", "091222333", "pepe@gmail.com", "masculino", _fecha);
            vendedor1.GestionSistema.AgregarCliente(cliente);
            
            vendedor1.AsignarOtroVendedor(vendedor2, cliente);
            Assert.That(vendedor1.GestionSistema.Clientes.Count, Is.EqualTo(0));
            Assert.That(vendedor2.GestionSistema.Clientes.Count, Is.EqualTo(1));
            Assert.That(vendedor2.GestionSistema.Clientes.Contains(cliente));
        }
        
        [Test]
        public void AsignarOtroVendedorIncorrectoTest()
        {
            var vendedor1 = new Vendedor("juan", "juan@gmail.com", "099222333",_gestionSistema);
            var vendedor2 = new Vendedor("juan2", "juan@gmail.com", "099222333",_gestionSistema);
            
            var cliente = new Cliente("Pepe", "Rodriguez", "091222333", "pepe@gmail.com", "masculino", _fecha);
            vendedor1.GestionSistema.AgregarCliente(cliente);
            
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
        
            vendedor2.AsignarOtroVendedor(vendedor1, cliente);
            string output = consoleOutput.ToString();
        
            Assert.That(output.Contains("ERROR: El cliente no existe"));
            Assert.That(vendedor2.GestionSistema.Clientes.Count, Is.EqualTo(0));
        }
    }
}