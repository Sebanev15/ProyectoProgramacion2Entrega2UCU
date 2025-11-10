  using Library.interfaces;
using NUnit.Framework;
using Library;
using System;
using System.Collections.Generic;



namespace LibraryTest
{
    public class ClienteTests
    {
        private GestionUsuario gestionUsuario;
        private Cliente j;
        private Usuario usuario;
        private IImporte venta;
        private IInteraccion mensaje;
        [SetUp]
        public void Setup()
        {
            gestionUsuario = new GestionUsuario();
            usuario = new Usuario("perez", "perez@gmail.como", "099477123", gestionUsuario);
            j = new Cliente("Juan", "Sanchez", "099477123", "correo@mail.com", "Masculino", new DateTime(1997, 10, 24));
            venta = new Venta("producto1", new DateTime(2021, 10, 24), 10, j);
            mensaje = new Mensaje(new DateTime(2021, 10, 24),"alo" , j, usuario, true);

        }
        
        [Test]
        public void ConstructorTest()
        {
            Assert.That(j.Nombre, Is.EqualTo("Juan"));
            Assert.That(j.Apellido, Is.EqualTo("Sanchez"));
            Assert.That(j.Telefono, Is.EqualTo("099477123"));
            Assert.That(j.Correo, Is.EqualTo("correo@mail.com"));
            Assert.That(j.Genero, Is.EqualTo("Masculino"));
            Assert.That(j.FechaDeNacimiento, Is.EqualTo(new DateTime(1997, 10, 24)));
        }
        [Test]
        public void DeberiaPermitirAsignarListasDeEtiquetasEInteracciones()
        {

            var etiquetas = new List<Etiqueta> { new Etiqueta("VIP") };
            var interacciones = new List<IInteraccion>();
            var importes = new List<IImporte>();

            j.Etiquetas = etiquetas;
            j.Interacciones = interacciones;
            j.Importes = importes;
        
            //Assert.That(j.Etiquetas.Count(), Is.EqualTo(1));
            Assert.That(j.Etiquetas[0].NombreEtiqueta, Is.EqualTo("VIP"));
            Assert.That(j.Interacciones, Is.EqualTo(interacciones));
            Assert.That(j.Importes, Is.EqualTo(importes));
        }

        [Test]
        public void DeberiaPermitirAgregarImporteSinDuplicar()
        {
            
            j.AgregarImporte(venta);
            Assert.That(j.Importes.Count, Is.EqualTo(1));
            Assert.That(j.Importes[0], Is.EqualTo(venta));
            j.AgregarImporte(venta);
            Assert.That(j.Importes.Count, Is.EqualTo(1));
        }
        
        [Test]
        public void DeberiaPermitirAgregarInteraccionesSinDuplicar()
        {
            
            j.RegistrarInteraccion(mensaje);
            Assert.That(j.Importes.Count, Is.EqualTo(1));
            Assert.That(j.Importes[0], Is.EqualTo(venta));
            j.AgregarImporte(venta);
            Assert.That(j.Importes.Count, Is.EqualTo(1));
        }

        [Test]
        public void VentasTotalesEnRangoEspecificoEncontradas()
        {
            venta = new Venta("producto1", new DateTime(2021, 10, 24), 10, j);
            Venta venta1 = new Venta("producto2", new DateTime(2023, 10, 24), 10, j);
            DateTime inicio=new DateTime(2020, 10, 24);
            DateTime fin=new DateTime(2023, 10, 24);
            j.AgregarImporte(venta);
            j.AgregarImporte(venta1);
            string ventas= j.ObtenerVentasTotales(inicio, fin);
            Assert.That(ventas, Is.EqualTo("Juan: MontoTotal=20, cantidad de ventas=2"));
        }

    }
}
