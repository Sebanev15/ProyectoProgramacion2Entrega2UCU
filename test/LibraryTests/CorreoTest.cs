using Library;
using NUnit.Framework;
using System;

namespace LibraryTests
{
    [TestFixture]
    public class CorreoTest
    {
        private Correo correito;
        private DateTime randomDate;
        private Cliente clientito;
        private Usuario usuarito;
    
        [SetUp]
        public void Setup()
        {
            randomDate = new DateTime(2025, 1, 1, 10, 30, 0);
            clientito = new Cliente("Juana", "de Arco", "12345678", "juanitayasabes@gmail.com", "F", randomDate);
            usuarito = new Usuario("josefina", "josefina@gmail.com", "87654321", new GestionUsuario());
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

        [Test]
        public void AgregarComentarioTest()
        {
            Assert.That(correito.Comentarios.Count, Is.EqualTo(0));
            correito.AgregarComentario("Primer comentario");
            Assert.That(correito.Comentarios.Count, Is.EqualTo(1));
            Assert.That(correito.Comentarios[0], Is.EqualTo("Primer comentario"));
            correito.AgregarComentario("Segundo comentario");
            Assert.That(correito.Comentarios.Count, Is.EqualTo(2));
            Assert.That(correito.Comentarios[1], Is.EqualTo("Segundo comentario"));
        }
    }
}
