using Library;
using NUnit.Framework;
using Library.interfaces;
using System;

namespace LibraryTest
{
    public class CotizacionTests
    {
        private Cotizacion c;
        private IClienteBase j;

        [SetUp]
        public void Setup()
        {
            j = new Cliente("Juan", "Sanchez", "099477123", "correo@mail.com", "Masculino", new DateTime(1997, 10, 24));
            c = new Cotizacion(new DateTime(2025, 10, 20), 2000.0, j);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.That(c.Fecha, Is.EqualTo(new DateTime(2025, 10, 20)));
            Assert.That(c.Monto, Is.EqualTo(2000.0));
            Assert.That(c.Cliente, Is.EqualTo(j));
        }
    }
}

