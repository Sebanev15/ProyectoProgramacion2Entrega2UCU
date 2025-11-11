using Library.interfaces;
using NUnit.Framework;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LibraryTest
{
    public class ClienteTests
    {
        private Cliente j;
        private Cliente abril;
        private IImporte importe;
        private Etiqueta etiqueta;
        private IInteraccion llamada;
        private Usuario user;
        private GestionUsuario _gestionUsuario;
        private GestionCliente _gestionCliente;
        
        [SetUp]
        public void Setup()
        {
            user = new Usuario("nombre", "correo", "31212", _gestionUsuario, _gestionCliente);
            etiqueta = new Etiqueta("nuevo");
            DateTime fecha = new DateTime(2024, 10, 20);
            importe = new Venta("algo", fecha, 500.0, j);
            j = new Cliente("Juan", "Sanchez", "099477703", "correo@mail.com", "M", new DateTime(1997, 10, 24));
            abril = new Cliente("Abril", "Sortez", "099477123", "abril@mail.com", "F", new DateTime(2002, 5, 2));
            llamada = new Llamada(fecha, "tema", j, user);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.That(j.Nombre, Is.EqualTo("Juan"));
            Assert.That(j.Apellido, Is.EqualTo("Sanchez"));
            Assert.That(j.Telefono, Is.EqualTo("099477703"));
            Assert.That(j.Correo, Is.EqualTo("correo@mail.com"));
            Assert.That(j.Genero, Is.EqualTo("M"));
            Assert.That(j.FechaDeNacimiento, Is.EqualTo(new DateTime(1997, 10, 24)));
        }
        [Test]
        public void DeberiaPermitirAsignarListasDeEtiquetasEInteracciones()
        {

            var etiquetas = new List<Etiqueta> { new Etiqueta("VIP") };
            var interacciones = new List<IInteraccion>();
            var importes = new List<IImporte>();

            j.Etiquetas = etiquetas;
            j.Interacciones = interacciones;
            j.Importes = importes;
        
            //Assert.That(j.Etiquetas.Count(), Is.EqualTo(1));
            Assert.That(j.Etiquetas[0].NombreEtiqueta, Is.EqualTo("VIP"));
            Assert.That(j.Interacciones, Is.EqualTo(interacciones));
            Assert.That(j.Importes, Is.EqualTo(importes));
        }

        [Test]
        public void AgregarImporte()
        {
            Assert.That(abril.Importes.Contains(importe), Is.False);
            abril.AgregarImporte(importe);
            Assert.That(abril.Importes.Contains(importe), Is.True);
        }
        
        [Test]
        public void AgregarEtiqueta()
        {
            Assert.That(abril.Etiquetas.Contains(etiqueta), Is.False);
            abril.AgregarEtiqueta(etiqueta);
            Assert.That(abril.Etiquetas.Contains(etiqueta), Is.True);
        }
        
        [Test]
        public void RegistrarInteraccion()
        {
            Assert.That(j.Interacciones.Contains(llamada),Is.False);
            j.RegistrarInteraccion(llamada);
            Assert.That(j.Interacciones.Contains(llamada),Is.True);

        }
        [Test]
        public void BuscarInteracciones()
        {                      
            Assert.That(!j.BuscarInteracciones(new DateTime(2024,10,20), "tema").Contains(llamada),Is.True);
            j.RegistrarInteraccion(llamada);
            Assert.That(j.BuscarInteracciones(new DateTime(2024,10,20), "tema").Contains(llamada),Is.True);

        }

        [Test]
        public void ObtenerVentasTotales()
        {
            j.AgregarImporte(importe);
            Assert.That(j.ObtenerVentasTotales(new DateTime(2024, 10, 19),new DateTime(2025, 10, 20)),
                Is.EqualTo("Juan: MontoTotal=500,0, cantidad de ventas=1 "));
        }

        [Test]
        public void ModificarDatos()
        {           
            Assert.That(j.Nombre, Is.EqualTo("Juan"));
            Assert.That(j.Apellido, Is.EqualTo("Sanchez"));
            Assert.That(j.Telefono, Is.EqualTo("099477703"));
            Assert.That(j.Correo, Is.EqualTo("correo@mail.com"));
            Assert.That(j.Genero, Is.EqualTo("M"));
            Assert.That(j.FechaDeNacimiento, Is.EqualTo(new DateTime(1997, 10, 24)));
            Cliente clienteModificado = new Cliente("Jaime", "Sanches", "099477700", "correo@mail.com", "M",
                new DateTime(1997, 10, 24));
            j.ModificarDatos(clienteModificado);
            Assert.That(j.Nombre, Is.EqualTo("Jaime"));
            Assert.That(j.Apellido, Is.EqualTo("Sanches"));
            Assert.That(j.Telefono, Is.EqualTo("099477700"));
            Assert.That(j.Correo, Is.EqualTo("correo@mail.com"));
            Assert.That(j.Genero, Is.EqualTo("M"));
            Assert.That(j.FechaDeNacimiento, Is.EqualTo(new DateTime(1997, 10, 24)));
        }
    }
}
