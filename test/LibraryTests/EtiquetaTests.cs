using Library;
using NUnit.Framework;
using Library.interfaces;
using System;
using System.Collections.Generic;

namespace LibraryTest
{
    public class EtiquetaTests
    {
        private Etiqueta e;
        private Cliente j;

        [SetUp]
        public void Setup()
        {
            j = new Cliente("Juan", "Sanchez", "099477123", "correo@mail.com", "Masculino", new DateTime(1997, 10, 24));
            e = new Etiqueta("Etiqueta");
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.That(e.NombreEtiqueta, Is.EqualTo("Etiqueta"));
        }

        [Test]
        public void ClientesTest()
        {
            Assert.That(e.Clientes, Is.Null);
        }
        [Test]
        public void ClientesAsignacionDeLista()
        {
            e.Clientes = new List<IClienteBase> { j };
        
            Assert.That(e.Clientes.Count, Is.EqualTo(1));
        }

    }
}
