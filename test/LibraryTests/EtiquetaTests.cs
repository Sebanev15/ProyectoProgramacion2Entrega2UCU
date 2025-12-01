using System;
using System.Collections.Generic;
using Library;
using NUnit.Framework;
using Ucu.Poo.DiscordBot.Domain;

namespace LibraryTests
{
    public class EtiquetaTests
    {
        private Etiqueta e;
        private Etiqueta erroneo;
        private Cliente j;

        [SetUp]
        public void Setup()
        {
            j = new Cliente("Juan", "Sanchez", "099477123", "correo@mail.com", "H", new DateTime(1997, 10, 24));
            e = new Etiqueta("Etiqueta");
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.That(e.NombreEtiqueta, Is.EqualTo("Etiqueta"));
            Assert.Throws<CampoInvalidoExepcion>(() =>
            {
                erroneo=new Etiqueta(null);
            });
        }

        [Test]
        public void ClientesTest()
        {
            Assert.That(e.Clientes, Is.Null);
        }
        [Test]
        public void ClientesAsignacionDeLista()
        {
            e.Clientes = new List<Cliente> { j };
        
            Assert.That(e.Clientes.Count, Is.EqualTo(1));
        }

        [Test]
        public void CrearEtiquetaVacia()
        {
            CampoInvalidoExepcion excepcion = Assert.Throws<CampoInvalidoExepcion>(() => new Etiqueta(null));
            Assert.That(excepcion.Message, Is.EqualTo("El nombre de la etiqueta no puede estar vacío."));
        }

    }
}
