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
    public class GestionClienteTest
    {

        private IInteraccion interaccion;
        private UsuarioBase usuarero;
        private GestionCliente _gestionCliente;
        private Cliente jorjito;
        private List<IImporte> Importes;
        private Cliente jorge;
        private Venta venta;
        private Cotizacion cotizacion;


        [SetUp]
        public void SetUp()
        {
            
            _gestionCliente = new GestionCliente();
            DateTime fechaN = new DateTime(2024, 10, 20);
            DateTime fechaReunion = new DateTime(2024, 12, 20);
            usuarero = new Usuario("user", "usurer@gmail.com", "001", new GestionCliente());
            jorjito = new Cliente("jorjito", "perez", "00", "monson@gmail.com", "M", fechaN);
            jorjito.Interacciones = new List<IInteraccion>();
            interaccion = new Reunion( fechaReunion, "Reunion", jorjito, usuarero, "Montevideo");
            Importes = new List<IImporte>();
            jorge = new Cliente("jorge", "perez", "00", "monson@gmail.com", "M", fechaN);
            DateTime fecha = new DateTime(2024, 10, 20);
            DateTime fecha1 = new DateTime(2024, 11, 20);
            venta = new Venta("caja", fecha, 12, jorge);
            cotizacion = new Cotizacion(fecha1, 12, jorge);
            usuarero = new Usuario("user", "usurer@gmail.com", "001", new GestionCliente());

            Importes.Add(venta);
            Importes.Add(cotizacion);
            
            _gestionCliente.Importes = Importes;
        }
        
        [Test]
        public void RegistrarInteraccionTest()
        {
            Assert.That(jorjito.Interacciones.Count, Is.EqualTo(0));
            _gestionCliente.RegistrarInteraccion(jorjito, interaccion);
            Assert.That(jorjito.Interacciones.Count, Is.EqualTo(1));
        
        }

        [Test]
        public void BuscarInteraccionesTests()
        {
            DateTime fechaBusqueda = new DateTime(2024, 12, 20);
            interaccion.Comentarios = new List<string>();
            interaccion.Comentarios.Add("hola");
            _gestionCliente.Interacciones.Add(interaccion);
            List<IInteraccion> resultado= _gestionCliente.BuscarInteracciones(fechaBusqueda, "Reunion", jorjito);
            Assert.That(resultado.Count, Is.EqualTo(1));
        }

        [Test]
        public void AgregarComentarioTesting()
        {
            interaccion.Comentarios = new List<string>();
            _gestionCliente.AgregarComentarioInteraccion(interaccion, "Finalizada en 10m");
            Assert.That(interaccion.Comentarios.Contains("Finalizada en 10m"), Is.True);
        
        }
        [Test]
        public void ObtenerVentasTotalesTesting()
        {
            List<String> resultado = _gestionCliente.ObtenerVentasTotales(
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
            _gestionCliente.AgregarImporte(venta, jorge);
            Assert.That(jorge.Importes.Count, Is.EqualTo(1));
        }
        
         [Test]
         public void AgregarClienteDouble()
         {
             _gestionCliente.AgregarCliente(jorge);
             Assert.That(_gestionCliente.Clientes.Count, Is.EqualTo(1));
             _gestionCliente.AgregarCliente(jorge);
             Assert.That(_gestionCliente.Clientes.Count, Is.EqualTo(1));
         }
         
         [Test]
         public void ModificarCliente()
         {
             _gestionCliente.AgregarCliente(jorge);
             Assert.That(_gestionCliente.Clientes.Count, Is.EqualTo(1));
             _gestionCliente.AgregarCliente(jorjito);
             _gestionCliente.ModificarCliente(jorge, jorjito);
             Assert.That(jorge.Nombre, Is.EqualTo(jorjito.Nombre));
         }
         
         [Test]
         public void EliminarClienteDouble()
         {
             _gestionCliente.AgregarCliente(jorge);
             Assert.That(_gestionCliente.Clientes.Count, Is.EqualTo(1));
             _gestionCliente.EliminarCliente(jorge);
             Assert.That(_gestionCliente.Clientes.Count, Is.EqualTo(0));
             _gestionCliente.EliminarCliente(jorge);
             Assert.That(_gestionCliente.Clientes.Count, Is.EqualTo(0));
         }
         
         [Test]
         public void BuscarClienteTesting()
         {
             _gestionCliente.AgregarCliente(jorge);
             _gestionCliente.AgregarCliente(jorjito);
             Assert.That(_gestionCliente.Clientes.Count, Is.EqualTo(2));
             List<Cliente> resultado=_gestionCliente.BuscarCliente("jorjito");
             Assert.That(resultado.Contains(jorjito), Is.True);
         }
         [Test]
         public void ListarClientesTesting()
         {
             _gestionCliente.AgregarCliente(jorge);
             _gestionCliente.AgregarCliente(jorjito);
             Assert.That(_gestionCliente.Clientes.Count, Is.EqualTo(2));
             var sw = new StringWriter();
             Console.SetOut(sw);
             _gestionCliente.ListarClientes();
             string resultado = sw.ToString();
             Assert.That(resultado, Does.Contain("jorjito"));
             Assert.That(resultado, Does.Contain("jorge"));
         }
         
         [Test]
         public void AgregarEtiqueta()
         {
             _gestionCliente.AgregarCliente(jorge);
             Assert.That(_gestionCliente.Clientes.Count, Is.EqualTo(1));
             _gestionCliente.AgregarCliente(jorjito);
             _gestionCliente.AgregarEtiqueta(jorjito, new Etiqueta("comercio"));
             Assert.That(jorjito.Etiquetas.Count, Is.EqualTo(1));
         }
         [Test]
         public void ObtenerClientesInactivos()
         {
             _gestionCliente.AgregarCliente(jorge);
             _gestionCliente.AgregarCliente(jorjito);
             Assert.That(_gestionCliente.Clientes.Count, Is.EqualTo(2));
             DateTime fechaNueva = new DateTime(2024, 10, 20);
             jorge.Interacciones.Add(new Reunion(fechaNueva, "Reunion1", jorge, usuarero, "Eiffel" ));
             List<Cliente> resultado= _gestionCliente.ObtenerClientesInactivos();
             Assert.That(resultado.Count, Is.EqualTo(1));
         }
         
         [Test]
         public void ObtenerClientesNoRespondidosTesting()
         {
             _gestionCliente.AgregarCliente(jorge);
             _gestionCliente.AgregarCliente(jorjito);
             Assert.That(_gestionCliente.Clientes.Count, Is.EqualTo(2));
             DateTime fechaNueva = new DateTime(2024, 10, 20);
             Reunion reunion = new Reunion(fechaNueva, "Reunion1", jorge, usuarero, "Eiffel");
             jorge.Interacciones.Add(reunion);
             
             List<Cliente> resultado= _gestionCliente.ObtenerClientesNoRespondidos();
             Assert.That(resultado.Count, Is.EqualTo(1));
             
             string comentario = "Esta reunion fue respondida";
             reunion.Comentarios.Add(comentario);
             resultado = _gestionCliente.ObtenerClientesNoRespondidos();
             Assert.That(resultado.Count, Is.EqualTo(0));
         }
    }
}