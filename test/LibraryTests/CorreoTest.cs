using Library;
using NUnit.Framework;
using Library.abstractions;
using System;

namespace LibraryTests
{
    [TestFixture]
    public class CorreoTest
    {
        private Correo correito;
        private DateTime randomDate;
        private Cliente clientito;
        private UsuarioBase usuarito;
    
        [SetUp]
        public void Setup()
        {
            randomDate = new DateTime(2025, 1, 1, 10, 30, 0);
            clientito = new Cliente("Juana", "de Arco", "12345678", "juanitayasabes@gmail.com", "F", randomDate);
            usuarito = new Usuario("josefina", "josefina@gmail.com", "87654321", new GestionSistema());
            correito = new Correo(randomDate, "holaholahola", clientito, usuarito, true);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.That(correito.Fecha, Is.EqualTo(randomDate));
            Assert.That(correito.Tema, Is.EqualTo("holaholahola"));
            Assert.That(correito.Cliente, Is.EqualTo(clientito));
            Assert.That(correito.Usuario, Is.EqualTo(usuarito));
            Assert.That(correito.EsEnviado, Is.EqualTo(true));
        }
    }
}
