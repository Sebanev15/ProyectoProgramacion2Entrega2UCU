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
    public class GestionSistemaTest
    {

        private IInteraccion interaccion;
        private UsuarioBase usuarero;
        private GestionSistema _gestionSistema;
        private Cliente jorjito;
        private List<IImporte> Importes;
        private Cliente jorge;
        private Venta venta;
        private Cotizacion cotizacion;


        [SetUp]
        public void SetUp()
        {
            
            _gestionSistema = new GestionSistema();
            DateTime fechaN = new DateTime(2024, 10, 20);
            DateTime fechaReunion = new DateTime(2024, 12, 20);
            usuarero = new Usuario("user", "usurer@gmail.com", "001");
            jorjito = new Cliente("jorjito", "perez", "00", "monson@gmail.com", "M", fechaN);
            jorjito.Interacciones = new List<IInteraccion>();
            interaccion = new Reunion( fechaReunion, "Reunion", jorjito, usuarero, "Montevideo");
            Importes = new List<IImporte>();
            jorge = new Cliente("jorge", "perez", "00", "monson@gmail.com", "M", fechaN);
            DateTime fecha = new DateTime(2024, 10, 20);
            DateTime fecha1 = new DateTime(2024, 11, 20);
            venta = new Venta("caja", fecha, 12, jorge);
            cotizacion = new Cotizacion(fecha1, 12, jorge);
            usuarero = new Usuario("user", "usurer@gmail.com", "001");

            Importes.Add(venta);
            Importes.Add(cotizacion);
            
            _gestionSistema.Importes = Importes;
        }
        
        [Test]
        public void RegistrarInteraccionTest()
        {
            Assert.That(jorjito.Interacciones.Count, Is.EqualTo(0));
            _gestionSistema.RegistrarInteraccion(jorjito, interaccion);
            Assert.That(jorjito.Interacciones.Count, Is.EqualTo(1));
        
        }

        [Test]
        public void BuscarInteraccionesTests()
        {
            DateTime fechaBusqueda = new DateTime(2024, 12, 20);
            interaccion.Comentarios = new List<string>();
            interaccion.Comentarios.Add("hola");
            _gestionSistema.Interacciones.Add(interaccion);
            List<IInteraccion> resultado= _gestionSistema.BuscarInteracciones(fechaBusqueda, "Reunion");
            Assert.That(resultado.Count, Is.EqualTo(1));
        }

        [Test]
        public void AgregarComentarioTesting()
        {
            interaccion.Comentarios = new List<string>();
            _gestionSistema.AgregarComentario(interaccion, "Finalizada en 10m");
            Assert.That(interaccion.Comentarios.Contains("Finalizada en 10m"), Is.True);
        
        }
        [Test]
        public void ObtenerVentasTotalesTesting()
        {
            List<IImporte> resultado = _gestionSistema.ObtenerVentasTotales(
                new DateTime(2024, 10, 20),
                new DateTime(2024, 12, 20)
            );

            Assert.That(resultado.Count, Is.EqualTo(2));

        }

        [Test]
        public void AgregarImporteTestCliente()
        {
            jorge.Importes = new List<IImporte>();
            Assert.That(jorge.Importes.Count, Is.EqualTo(0));
            _gestionSistema.AgregarImporte(venta, jorge);
            Assert.That(jorge.Importes.Count, Is.EqualTo(1));
        }
        
         [Test]
         public void AgregarClienteDouble()
         {
             _gestionSistema.AgregarCliente(jorge);
             Assert.That(_gestionSistema.Clientes.Count, Is.EqualTo(1));
             _gestionSistema.AgregarCliente(jorge);
             Assert.That(_gestionSistema.Clientes.Count, Is.EqualTo(1));
         }
         
         [Test]
         public void ModificarCliente()
         {
             _gestionSistema.AgregarCliente(jorge);
             Assert.That(_gestionSistema.Clientes.Count, Is.EqualTo(1));
             _gestionSistema.AgregarCliente(jorjito);
             _gestionSistema.ModificarCliente(jorge, jorjito);
             Assert.That(jorge.Nombre, Is.EqualTo(jorjito.Nombre));
         }
         
         [Test]
         public void EliminarClienteDouble()
         {
             _gestionSistema.AgregarCliente(jorge);
             Assert.That(_gestionSistema.Clientes.Count, Is.EqualTo(1));
             _gestionSistema.EliminarCliente(jorge);
             Assert.That(_gestionSistema.Clientes.Count, Is.EqualTo(0));
             _gestionSistema.EliminarCliente(jorge);
             Assert.That(_gestionSistema.Clientes.Count, Is.EqualTo(0));
         }
         
         [Test]
         public void BuscarClienteTesting()
         {
             _gestionSistema.AgregarCliente(jorge);
             _gestionSistema.AgregarCliente(jorjito);
             Assert.That(_gestionSistema.Clientes.Count, Is.EqualTo(2));
             List<Cliente> resultado=_gestionSistema.BuscarCliente("jorjito");
             Assert.That(resultado.Contains(jorjito), Is.True);
         }
         [Test]
         public void ListarClientesTesting()
         {
             _gestionSistema.AgregarCliente(jorge);
             _gestionSistema.AgregarCliente(jorjito);
             Assert.That(_gestionSistema.Clientes.Count, Is.EqualTo(2));
             var sw = new StringWriter();
             Console.SetOut(sw);
             _gestionSistema.ListarClientes();
             string resultado = sw.ToString();
             Assert.That(resultado, Does.Contain("jorjito"));
             Assert.That(resultado, Does.Contain("jorge"));
         }
         
         [Test]
         public void AgregarEtiqueta()
         {
             _gestionSistema.AgregarCliente(jorge);
             Assert.That(_gestionSistema.Clientes.Count, Is.EqualTo(1));
             _gestionSistema.AgregarCliente(jorjito);
             _gestionSistema.AgregarEtiqueta(jorjito, new Etiqueta("comercio"));
             Assert.That(jorjito.Etiquetas.Count, Is.EqualTo(1));
         }
         [Test]
         public void ObtenerClientesInactivos()
         {
             _gestionSistema.AgregarCliente(jorge);
             _gestionSistema.AgregarCliente(jorjito);
             Assert.That(_gestionSistema.Clientes.Count, Is.EqualTo(2));
             DateTime fechaNueva = new DateTime(2024, 10, 20);
             jorge.Interacciones.Add(new Reunion(fechaNueva, "Reunion1", jorge, usuarero, "Eiffel" ));
             List<Cliente> resultado= _gestionSistema.ObtenerClientesInactivos();
             Assert.That(resultado.Count, Is.EqualTo(1));
         }
         
         [Test]
         public void ObtenerClientesNoRespondidosTesting()
         {
             _gestionSistema.AgregarCliente(jorge);
             _gestionSistema.AgregarCliente(jorjito);
             Assert.That(_gestionSistema.Clientes.Count, Is.EqualTo(2));
             DateTime fechaNueva = new DateTime(2024, 10, 20);
             Reunion reunion = new Reunion(fechaNueva, "Reunion1", jorge, usuarero, "Eiffel");
             jorge.Interacciones.Add(reunion);
             
             List<Cliente> resultado= _gestionSistema.ObtenerClientesNoRespondidos();
             Assert.That(resultado.Count, Is.EqualTo(1));
             
             string comentario = "Esta reunion fue respondida";
             reunion.Comentarios.Add(comentario);
             resultado = _gestionSistema.ObtenerClientesNoRespondidos();
             Assert.That(resultado.Count, Is.EqualTo(0));
         }
    }
}