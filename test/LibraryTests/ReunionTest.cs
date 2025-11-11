using Library;
using NUnit.Framework;
using System;

namespace LibraryTest
{
    [TestFixture]
    public class ReunionTest
    {
        private Reunion reunioncita;
        private DateTime randomDate;
        private Cliente clientito;
        private Usuario usuarito;
    
        [SetUp]
        public void Setup()
        {
            randomDate = new DateTime(2025, 1, 1, 10, 30, 0);
            clientito = new Cliente("Juana", "de Arco", "12345678", "juanitayasabes@gmail.com", "F", randomDate);
            usuarito = new Usuario("josefina", "josefina@gmail.com", "87654321", new GestionUsuario(),new GestionCliente());
            reunioncita = new Reunion(randomDate, "holaholahola", clientito, usuarito, "1234 Calle");
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.That(reunioncita.Fecha, Is.EqualTo(randomDate));
            Assert.That(reunioncita.Tema, Is.EqualTo("holaholahola"));
            Assert.That(reunioncita.Cliente, Is.EqualTo(clientito));
            Assert.That(reunioncita.Usuario, Is.EqualTo(usuarito));
            Assert.That(reunioncita.Direccion, Is.EqualTo("1234 Calle"));
        }
    }
}