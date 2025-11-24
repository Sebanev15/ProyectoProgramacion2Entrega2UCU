using System;
using Library;
using NUnit.Framework;

namespace LibraryTests
{
    [TestFixture]
    public class CotizacionTests
    {
        private Cotizacion c;
        private Cotizacion c2;
        private Cliente j;
        private Cliente j2;

        [SetUp]
        public void Setup()
        {
            j = new Cliente("Juan", "Sanchez", "099477123", "correo@mail.com", "Masculino", new DateTime(1997, 10, 24));
            j2 = new Cliente("Juan2", "Sanchez", "099477123", "correo@mail.com", "Masculino", new DateTime(1997, 10, 24));

            c = new Cotizacion(new DateTime(2025, 10, 20), 2000.0, j);
            c2 = new Cotizacion(new DateTime(2022, 10, 21), 1000.0, j2);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.That(c.Fecha, Is.EqualTo(new DateTime(2025, 10, 20)));
            Assert.That(c.Monto, Is.EqualTo(2000.0));
            Assert.That(c.Cliente, Is.EqualTo(j));
            Assert.That(c2.Fecha, Is.EqualTo(new DateTime(2022, 10, 21)));
            Assert.That(c2.Monto, Is.EqualTo(c2.Monto));
            Assert.That(c2.Cliente, Is.EqualTo(j2));
        }
        
        [Test]
        public void ModificarCotizacionRetornaDatosCorrectos()
        {
            c.ModificarImporte(c2);
            Assert.That(c.Monto, Is.EqualTo(c2.Monto));
            Assert.That(c.Cliente, Is.EqualTo(c2.Cliente));
            Assert.That(c.Fecha, Is.EqualTo(c2.Fecha));
        }
    }
}

