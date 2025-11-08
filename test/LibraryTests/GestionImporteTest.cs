using Library;
using NUnit.Framework;
using Library.interfaces;
using System;
using System.Collections.Generic;


namespace LibraryTest
{
    public class GestionImporteTest
    {
        private List<IImporte> Importes;
        private Venta venta;
        private Cotizacion cotizacion;
        private GestionImporte _gestionImporte;
        private IClienteBase jorge;
    
        [SetUp]
        public void Setup()
        {
            Importes = new List<IImporte>();

            DateTime fechaN = new DateTime(2024, 10, 20);
            jorge = new Cliente("jorge", "perez", "00", "monson@gmail.com", "M", fechaN);

            DateTime fecha = new DateTime(2024, 10, 20);
            DateTime fecha1 = new DateTime(2024, 11, 20);
            venta = new Venta("caja", fecha, 12, jorge);
            cotizacion = new Cotizacion(fecha1, 12, jorge);

            Importes.Add(venta);
            Importes.Add(cotizacion);

            _gestionImporte = new GestionImporte();
            _gestionImporte.Importes = Importes;
        }

    
        [Test]
        public void ObtenerVentasTotalesTesting()
        {
            List<IImporte> resultado = _gestionImporte.ObtenerVentasTotales(
                new DateTime(2024, 10, 20),
                new DateTime(2024, 12, 20)
            );

            Assert.That(resultado.Count, Is.EqualTo(2));

        }

        [Test]
        public void AgregarImporteTestCliente()
        {
            jorge.Importes = new List<IImporte>();
            Assert.That(jorge.Importes.Count, Is.EqualTo(0));
            _gestionImporte.AgregarImporte(venta, jorge);
            Assert.That(jorge.Importes.Count, Is.EqualTo(1));
        }
    }
}
