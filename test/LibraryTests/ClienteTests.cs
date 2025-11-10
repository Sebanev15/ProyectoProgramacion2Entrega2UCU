  using Library.interfaces;
using NUnit.Framework;
using Library;
using System;
using System.Collections.Generic;
using Library.abstractions;


namespace LibraryTest
{
    public class ClienteTests
    {
        private Cliente j;
        private Usuario usuario;
        private IImporte venta;
        private IInteraccion mensaje;
        [SetUp]
        public void Setup()
        {
            j = new Cliente("Juan", "Sanchez", "099477123", "correo@mail.com", "Masculino", new DateTime(1997, 10, 24));
            venta = new Venta("producto1", new DateTime(2021, 10, 24), 10, j);
        }
        
        [Test]
        public void ConstructorTest()
        {
            Assert.That(j.Nombre, Is.EqualTo("Juan"));
            Assert.That(j.Apellido, Is.EqualTo("Sanchez"));
            Assert.That(j.Telefono, Is.EqualTo("099477123"));
            Assert.That(j.Correo, Is.EqualTo("correo@mail.com"));
            Assert.That(j.Genero, Is.EqualTo("Masculino"));
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
        public void DeberiaPermitirAgregarImporteSinDuplicar()
        {
            
            j.AgregarImporte(venta);
            Assert.That(j.Importes.Count, Is.EqualTo(1));
            Assert.That(j.Importes[0], Is.EqualTo(venta));
            j.AgregarImporte(venta);
            Assert.That(j.Importes.Count, Is.EqualTo(1));
        }
        
        [Test]
        public void DeberiaPermitirAgregarInteraccionesSinDuplicar()
        {
            
            j.RegistrarInteraccion(mensaje);
            Assert.That(j.Importes.Count, Is.EqualTo(1));
            Assert.That(j.Importes[0], Is.EqualTo(venta));
            j.AgregarImporte(venta);
            Assert.That(j.Importes.Count, Is.EqualTo(1));
        }

      
    }
}
