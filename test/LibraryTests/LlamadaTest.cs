using Library;
using NUnit.Framework;
using Library.abstractions;
using System;

namespace LibraryTest
{
    [TestFixture]
    public class LlamadaTest
    {
        private Llamada llamadita;
        private DateTime randomDate;
        private Cliente clientito;
        private UsuarioBase usuarito;
    
        [SetUp]
        public void Setup()
        {
            randomDate = new DateTime(2025, 1, 1, 10, 30, 0);
            clientito = new Cliente("Juana", "de Arco", "12345678", "juanitayasabes@gmail.com", "F", randomDate);
            usuarito = new Usuario("josefina", "josefina@gmail.com", "87654321");
            llamadita = new Llamada(randomDate, "holaholahola", clientito, usuarito);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.That(llamadita.Fecha, Is.EqualTo(randomDate));
            Assert.That(llamadita.Tema, Is.EqualTo("holaholahola"));
            Assert.That(llamadita.Cliente, Is.EqualTo(clientito));
            Assert.That(llamadita.Usuario, Is.EqualTo(usuarito));
        }
    }
}