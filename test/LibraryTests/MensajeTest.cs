using Library;
using NUnit.Framework;
using Library.abstractions;
using System;

namespace LibraryTest
{
    [TestFixture]
    public class MensajeTest
    {
        private Mensaje mensajito;
        private DateTime randomDate;
        private Cliente clientito;
        private Usuario usuarito;
    
        [SetUp]
        public void Setup()
        {
            randomDate = new DateTime(2025, 1, 1, 10, 30, 0);
            clientito = new Cliente("Juana", "de Arco", "12345678", "juanitayasabes@gmail.com", "F", randomDate);
            usuarito = new Usuario("josefina", "josefina@gmail.com", "87654321", new GestionSistema());
            mensajito = new Mensaje(randomDate, "holaholahola", clientito, usuarito, true);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.That(mensajito.Fecha, Is.EqualTo(randomDate));
            Assert.That(mensajito.Tema, Is.EqualTo("holaholahola"));
            Assert.That(mensajito.Cliente, Is.EqualTo(clientito));
            Assert.That(mensajito.Usuario, Is.EqualTo(usuarito));
            Assert.That(mensajito.EsEnviado, Is.EqualTo(true));
        }
    }
}
