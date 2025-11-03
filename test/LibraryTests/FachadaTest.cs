using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using NUnit.Framework;
using Library;
using Library.interfaces;
using Library.abstractions;

namespace Library.Tests
{
    [TestFixture]
    public class FachadaTest
    {
        private GestionImporte _gestionImporte;
        private GestionInteraccion _gestionInteraccion;
        private GestionCliente _gestionCliente;
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
            _gestionImporte = new GestionImporte();
            _gestionInteraccion = new GestionInteraccion();
            _gestionCliente = new GestionCliente();
            _fecha = new DateTime(2024, 10, 1);
            _cliente = new Cliente("juan", "smith", "12345678", "juansmith007@gmail.com", "M",_fecha);
            _fachada = new Fachada(_gestionImporte, _gestionInteraccion, _gestionCliente);
            _usuario = new Usuario("Usuariooo", "usuario@mail.com", "23423423");
            _interaccion = new Correo(_fecha, "importante", _cliente, _usuario, true);
            _admin = new Administrador("Mauro", "mauroeladmin@gmail.com", "12341234");
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
            Assert.IsFalse(original.Etiquetas[0]==modificado.Etiquetas[0]);

        }

        [Test]
        public void AgregarEliminarClienteFachadaTest()
        {
            var fecha = _fecha;
            var cliente = _cliente;
            _fachada.AgregarCliente(cliente);
            Assert.That(_gestionCliente.Clientes, Does.Contain(cliente));
            _fachada.EliminarCliente(cliente);
            Assert.That(_gestionCliente.Clientes, Does.Not.Contain(cliente));
        }

        [Test]
        public void BuscarClienteFachadaTest()
        {
            var resultado = _fachada.BuscarCliente("Pedro");
            Assert.That(resultado, Is.EqualTo(_gestionCliente.BuscarCliente("Pedro")));
        }

        [Test]
        public void AgregarEtiquetaFachadaTest()
        {
            var cliente = _cliente;
            var etiqueta = new Etiqueta("Premium"); GestionCliente gestionCliente = new GestionCliente();

            _fachada.AgregarEtiqueta(cliente, etiqueta);

            Assert.That(cliente.Etiquetas.Contains(etiqueta));
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
        public void SuspenderReactivarUsuarioFachadaTest()
        {
            var usuario = _usuario;
            var admin = _admin;
            
            Fachada.AdminSuspenderUsuario(admin,usuario);
            Assert.That(usuario.EstaSuspendido, Is.True);
            Fachada.AdminReactivarUsuario(admin,usuario);
            Assert.That(usuario.EstaSuspendido, Is.False);
        }
    }
}