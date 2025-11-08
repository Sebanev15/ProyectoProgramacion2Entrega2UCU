using Library;
using NUnit.Framework;
using Library.interfaces;
using Library.abstractions;
using System;
using System.Collections.Generic;

namespace LibraryTest
{
    public class GestionInteraccionTest
    {

        private GestionInteraccionBase _gestionInteraccion ;
        private IInteraccion interaccion;
        private UsuarioBase usuarero;
        private IClienteBase jorjito;
        [SetUp]
        public void SetUp()
        {
            _gestionInteraccion = new GestionInteraccion();
            _gestionInteraccion.Interacciones = new List<IInteraccion>();
            DateTime fechaN = new DateTime(2024, 10, 20);
            DateTime fechaReunion = new DateTime(2024, 12, 20);
            usuarero = new Usuario("user", "usurer@gmail.com", "001");
            jorjito = new Cliente("jorge", "perez", "00", "monson@gmail.com", "M", fechaN);
            jorjito.Interacciones = new List<IInteraccion>();
            interaccion = new Reunion( fechaReunion, "Reunion", jorjito, usuarero, "Montevideo");

        }

    
    
    
        [Test]
        public void RegistrarInteraccionTest()
        {
            Assert.That(jorjito.Interacciones.Count, Is.EqualTo(0));
            _gestionInteraccion.RegistrarInteraccion(jorjito, interaccion);
            Assert.That(jorjito.Interacciones.Count, Is.EqualTo(1));
        
        }

        [Test]
        public void BuscarInteraccionesTests()
        {
            DateTime fechaBusqueda = new DateTime(2024, 12, 20);
            interaccion.Comentarios = new List<string>();
            interaccion.Comentarios.Add("hola");
            _gestionInteraccion.Interacciones.Add(interaccion);
            List<IInteraccion> resultado= _gestionInteraccion.BuscarInteracciones(fechaBusqueda, "Reunion");
            Assert.That(resultado.Count, Is.EqualTo(1));
        }

        [Test]
        public void AgregarComentarioTesting()
        {
            interaccion.Comentarios = new List<string>();
            _gestionInteraccion.AgregarComentario(interaccion, "Finalizada en 10m");
            Assert.That(interaccion.Comentarios.Contains("Finalizada en 10m"), Is.True);
        
        }

    }
}
