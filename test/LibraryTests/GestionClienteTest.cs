using System;
using System.Collections.Generic;
using System.IO;
using Library;
using Library.interfaces;
using NUnit.Framework;
using Ucu.Poo.DiscordBot.Domain;

namespace LibraryTests
{
    [TestFixture]
    public class GestionClienteTest
    {

        private IInteraccion interaccion;
        private Usuario usuarero;
        private GestionCliente _gestionCliente;
        private Cliente jorjito;
        private Cliente jorjito2;
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

            usuarero = new Usuario("user", "usurer@gmail.com", "001", new GestionUsuario(), new GestionCliente());

            jorjito = new Cliente("jorjito", "perez", "00", "monson@gmail.com", "M", fechaN);
            jorjito2 = new Cliente("jorjito", "perez", "01", "monson@gmail.com", "M", fechaN);
            jorjito.Interacciones = new List<IInteraccion>();
            interaccion = new Reunion( fechaReunion, "Reunion", jorjito, usuarero, "Montevideo");
            Importes = new List<IImporte>();
            jorge = new Cliente("jorge", "perez", "01", "monson@gmail.com", "M", fechaN);
            DateTime fecha = new DateTime(2024, 10, 20);
            DateTime fecha1 = new DateTime(2024, 11, 20);
            venta = new Venta("caja", fecha, 12, jorge);
            cotizacion = new Cotizacion(fecha1, 12, jorge);

            usuarero = new Usuario("user", "usurer@gmail.com", "001", new GestionUsuario(),new GestionCliente());


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
            
            _gestionCliente.AgregarComentarioInteraccion(interaccion, "Importante reunion");
            _gestionCliente.RegistrarInteraccion(jorjito, interaccion);
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
            _gestionCliente.AgregarCliente(jorge);
            List<String> resultado = _gestionCliente.ObtenerVentasTotales(
                new DateTime(2024, 10, 19),
                new DateTime(2024, 12, 20) 
            );

            Assert.That(resultado.Count, Is.EqualTo(1));

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
             _gestionCliente.AgregarCliente(jorjito2);
             Assert.That(_gestionCliente.Clientes.Count, Is.EqualTo(3));
             List<string> datos = new List<string>();
             datos.Add("jorjito");
             datos.Add("00");
             List<Cliente> resultado=_gestionCliente.BuscarCliente(datos);
            
             Assert.That(resultado.Contains(jorjito), Is.True);
             Assert.That(resultado.Contains(jorjito2), Is.False);
             
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
         public void ListarClientesConReturnTesting()
         {
             _gestionCliente.AgregarCliente(jorge);
             _gestionCliente.AgregarCliente(jorjito);
             Assert.That(_gestionCliente.Clientes.Count, Is.EqualTo(2));
             List<Cliente> resultado= _gestionCliente.ListarClientesConReturn();
             Assert.That(resultado.Count, Is.EqualTo(2));
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
             ListaVaciaExcepcion excepcion = Assert.Throws<ListaVaciaExcepcion>(() => _gestionCliente.ObtenerClientesNoRespondidos());
             Assert.That(excepcion.Message, Is.EqualTo("No hay clientes sin responder"));
         }

         [Test]
         public void EliminarImporteCorrectoTest()
         {
                _gestionCliente.EliminarImporte(venta);
                Assert.That(_gestionCliente.Importes.Contains(venta), Is.False);
         }
         [Test]
         public void EliminarImporteNoExistenteTest()
         {
             IImporte importeNoExistente = new Venta("noExiste", DateTime.Now, 50, jorge);
             Assert.That(this.Importes.Contains(importeNoExistente), Is.False);
             _gestionCliente.EliminarImporte(importeNoExistente);
             Assert.That(this.Importes.Contains(importeNoExistente), Is.False);
         }
         
         [Test]
         public void ModificarImporteTest()
         {
             IImporte importeModificado = new Venta("cajaModificada", venta.Fecha, 20, jorge);
             _gestionCliente.ModificarImporte(venta, importeModificado);
             Assert.That(venta.Producto, Is.EqualTo("cajaModificada"));
             Assert.That(venta.Monto, Is.EqualTo(20));
             Assert.That(_gestionCliente.Importes.Contains(venta), Is.True);
         }
         
         [Test]
         public void BuscarCotizacionesSinFechaTest_ProductoNoAplica()
         {
             List<string> datosBusqueda = new List<string> { "caja" };
             List<Cotizacion> resultado = _gestionCliente.BuscarCotizacionesSinFecha(datosBusqueda);
             Assert.That(resultado.Count, Is.EqualTo(0)); 
         }
         
         [Test]
         public void BuscarCotizacionesSinFechaTest()
         {
             List<string> datosBusqueda = new List<string> { "12" };
             List<Cotizacion> resultado = _gestionCliente.BuscarCotizacionesSinFecha(datosBusqueda);
             Assert.That(resultado.Count, Is.EqualTo(1));
             Assert.That(resultado[0], Is.EqualTo(cotizacion));
         }
        
         [Test]
         public void BuscarCotizacionesSinFecha_SinResultados()
         {
             List<string> datosBusqueda = new List<string> { "noExiste", "999" };
             List<Cotizacion> resultado = _gestionCliente.BuscarCotizacionesSinFecha(datosBusqueda);
             Assert.That(resultado.Count, Is.EqualTo(0));
         }
         
[Test]
         public void BuscarVentasSinFecha_SinResultados()
         {
             List<string> datosBusqueda = new List<string> { "productoInexistente" };
             List<Venta> resultado = _gestionCliente.BuscarVentasSinFecha(datosBusqueda);
             Assert.That(resultado.Count, Is.EqualTo(0));
         }
         
         [Test]
         public void BuscarVentasSinFecha_VariosResultados()
         {
             DateTime fecha2 = new DateTime(2024, 12, 15);
             Venta venta2 = new Venta("caja", fecha2, 25, jorjito);
             _gestionCliente.Importes.Add(venta2);
             
             List<string> datosBusqueda = new List<string> { "caja" };
             List<Venta> resultado = _gestionCliente.BuscarVentasSinFecha(datosBusqueda);
             Assert.That(resultado.Count, Is.EqualTo(2));
         }

         [Test]
         public void ObtenerClientesVentasMayoresYMenoresTest()
         {
             var juan = new Cliente("juan","juan", "123123123", "juan@juan.juan", "H", DateTime.Now);
             var juan2 = new Cliente("juan","juan", "123123123", "juan@juan.juan", "H", DateTime.Now);
             var gestionCliente = new GestionCliente();
             gestionCliente.AgregarCliente(juan);
             gestionCliente.AgregarCliente(juan2);
             var venta1 = new Venta("hola", DateTime.Now, 200,juan);
             var venta2 = new Venta("hola", DateTime.Now, 50,juan2);
             gestionCliente.AgregarImporte(venta1,juan);
             gestionCliente.AgregarImporte(venta2,juan2);
             
             var listResult = gestionCliente.ObtenerClientesVentasMayoresA(100);
             Assert.That(listResult.Count, Is.EqualTo(1));
             Assert.That(listResult.Contains(juan), Is.True);
             Assert.That(listResult.Contains(juan2), Is.False);
             
             listResult = gestionCliente.ObtenerClientesVentasMenoresA(100);
             Assert.That(listResult.Count, Is.EqualTo(1));
             Assert.That(listResult.Contains(juan), Is.False);
             Assert.That(listResult.Contains(juan2), Is.True);
         }

         [Test]
         public void ObtenerClientesConVentasEnRangoTest()
         {
             var juan = new Cliente("juan","juan", "123123123", "juan@juan.juan", "H", DateTime.Now);
             var juan2 = new Cliente("juan","juan", "123123123", "juan@juan.juan", "H", DateTime.Now);
             var gestionCliente = new GestionCliente();
             gestionCliente.AgregarCliente(juan);
             gestionCliente.AgregarCliente(juan2);
             var venta1 = new Venta("hola", DateTime.Now, 200,juan);
             var venta2 = new Venta("hola", DateTime.Now, 50,juan2);
             gestionCliente.AgregarImporte(venta1,juan);
             gestionCliente.AgregarImporte(venta2,juan2);

             var listResult = gestionCliente.ObtenerClientesConVentasEnRango(0, 100);
             Assert.That(listResult.Count, Is.EqualTo(1));
             Assert.That(listResult.Contains(juan), Is.False);
             Assert.That(listResult.Contains(juan2), Is.True);
             
             listResult = gestionCliente.ObtenerClientesConVentasEnRango(100, 0);
             Assert.That(listResult.Count, Is.EqualTo(1));
             Assert.That(listResult.Contains(juan), Is.False);
             Assert.That(listResult.Contains(juan2), Is.True);
             
             listResult = gestionCliente.ObtenerClientesConVentasEnRango(100,300);
             Assert.That(listResult.Count, Is.EqualTo(1));
             Assert.That(listResult.Contains(juan), Is.True);
             Assert.That(listResult.Contains(juan2), Is.False);
         }

         [Test]
         public void ObtenerClientesConVentasDeProductoTest()
         {
             var juan = new Cliente("juan","juan", "123123123", "juan@juan.juan", "H", DateTime.Now);
             var juan2 = new Cliente("juan","juan", "123123123", "juan@juan.juan", "H", DateTime.Now);
             var gestionCliente = new GestionCliente();
             gestionCliente.AgregarCliente(juan);
             gestionCliente.AgregarCliente(juan2);
             var venta1 = new Venta("a", DateTime.Now, 200,juan);
             var venta2 = new Venta("hola", DateTime.Now, 50,juan2);
             gestionCliente.AgregarImporte(venta1,juan);
             gestionCliente.AgregarImporte(venta2,juan2);

             var listResult = gestionCliente.ObtenerClientesConVentasDeProducto("a");
             Assert.That(listResult.Count, Is.EqualTo(1));
             Assert.That(listResult.Contains(juan), Is.True);
             Assert.That(listResult.Contains(juan2), Is.False);
         }
    }
}