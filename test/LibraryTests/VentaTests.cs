using Library.interfaces;
using Library;
using System;
using NUnit.Framework;

namespace LibraryTest
{
    public class VentaTests
    {
        private Venta v;
        private Venta v2;
        private Cliente j;


        [SetUp]
        public void Setup()
        {
            j = new Cliente("Juan", "Sanchez", "099477123", "correo@mail.com", "Masculino", new DateTime(1997, 10, 24));
            v = new Venta("Producto", new DateTime(2025, 10,20),1500.00,j);
            v2 = new Venta("Producto2", new DateTime(2025, 10,20),1500.00,j);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.That(v.Producto, Is.EqualTo("Producto"));
            Assert.That(v.Fecha, Is.EqualTo(new DateTime(2025, 10,20)));
            Assert.That(v.Monto, Is.EqualTo(1500.00));
            Assert.That(v.Cliente, Is.EqualTo(j));
        }
       
        [Test]
        public void ModificarVentaRetornaDatosCorrectos()
        {
            v.ModificarImporte(v2);
            Assert.That(v.Producto, Is.EqualTo("Producto2"));
        }

       
        
    }
}
