using Library;
using NUnit.Framework;
using Library.interfaces;
using Library.abstractions;
using System;
using System.Collections.Generic;
using System.IO;

namespace LibraryTest
{
    public class GestionClienteTest
    {
         private GestionClienteBase pablo;
         private IClienteBase juanpe;
         private IClienteBase alfredo;
         private UsuarioBase usuarero;

         [SetUp]
         public void SetUp()
         {
             pablo = new GestionCliente();
             DateTime fechaN = new DateTime(2024, 10, 20);
             juanpe=  new Cliente("jorge", "perez", "00", "monson@gmail.com", "M", fechaN);
             juanpe.Interacciones = new List<IInteraccion>();
             juanpe.Etiquetas = new List<Etiqueta>();
             juanpe.Importes = new List<IImporte>();
             alfredo=  new Cliente("alfredo","siciliana", "02", "monson2@gmail.com", "F", fechaN);
             alfredo.Interacciones = new List<IInteraccion>();
             alfredo.Etiquetas = new List<Etiqueta>();
             alfredo.Importes = new List<IImporte>();
             usuarero = new Usuario("user", "usurer@gmail.com", "001");
         }

         [Test]
         public void AgregarClienteDouble()
         {
             pablo.AgregarCliente(juanpe);
             Assert.That(pablo.Clientes.Count, Is.EqualTo(1));
             pablo.AgregarCliente(juanpe);
             Assert.That(pablo.Clientes.Count, Is.EqualTo(1));
         }
         
         [Test]
         public void ModificarCliente()
         {
             pablo.AgregarCliente(juanpe);
             Assert.That(pablo.Clientes.Count, Is.EqualTo(1));
             pablo.AgregarCliente(alfredo);
             pablo.ModificarCliente(juanpe, alfredo);
             Assert.That(juanpe.Nombre, Is.EqualTo(alfredo.Nombre));
         }
         
         [Test]
         public void EliminarClienteDouble()
         {
             pablo.AgregarCliente(juanpe);
             Assert.That(pablo.Clientes.Count, Is.EqualTo(1));
             pablo.EliminarCliente(juanpe);
             Assert.That(pablo.Clientes.Count, Is.EqualTo(0));
             pablo.EliminarCliente(juanpe);
             Assert.That(pablo.Clientes.Count, Is.EqualTo(0));
         }
         
         [Test]
         public void BuscarClienteTesting()
         {
             pablo.AgregarCliente(juanpe);
             pablo.AgregarCliente(alfredo);
             Assert.That(pablo.Clientes.Count, Is.EqualTo(2));
             List<IClienteBase> resultado=pablo.BuscarCliente("alfredo");
             Assert.That(resultado.Contains(alfredo), Is.True);
         }
         [Test]
         public void ListarClientesTesting()
         {
             pablo.AgregarCliente(juanpe);
             pablo.AgregarCliente(alfredo);
             Assert.That(pablo.Clientes.Count, Is.EqualTo(2));
             var sw = new StringWriter();
             Console.SetOut(sw);
             pablo.ListarClientes();
             string resultado = sw.ToString();
             Assert.That(resultado, Does.Contain("alfredo"));
             Assert.That(resultado, Does.Contain("jorge"));
         }
         
         [Test]
         public void AgregarEtiqueta()
         {
             pablo.AgregarCliente(juanpe);
             Assert.That(pablo.Clientes.Count, Is.EqualTo(1));
             pablo.AgregarCliente(alfredo);
             pablo.AgregarEtiqueta(alfredo, new Etiqueta("comercio"));
             Assert.That(alfredo.Etiquetas.Count, Is.EqualTo(1));
         }
         [Test]
         public void ObtenerClientesInactivos()
         {
             pablo.AgregarCliente(juanpe);
             pablo.AgregarCliente(alfredo);
             Assert.That(pablo.Clientes.Count, Is.EqualTo(2));
             DateTime fechaNueva = new DateTime(2024, 10, 20);
             juanpe.Interacciones.Add(new Reunion(fechaNueva, "Reunion1", juanpe, usuarero, "Eiffel" ));
             List<IClienteBase> resultado= pablo.ObtenerClientesInactivos();
             Assert.That(resultado.Count, Is.EqualTo(1));
         }
         
         [Test]
         public void ObtenerClientesNoRespondidosTesting()
         {
             pablo.AgregarCliente(juanpe);
             pablo.AgregarCliente(alfredo);
             Assert.That(pablo.Clientes.Count, Is.EqualTo(2));
             DateTime fechaNueva = new DateTime(2024, 10, 20);
             Reunion reunion = new Reunion(fechaNueva, "Reunion1", juanpe, usuarero, "Eiffel");
             juanpe.Interacciones.Add(reunion);
             
             List<IClienteBase> resultado= pablo.ObtenerClientesNoRespondidos();
             Assert.That(resultado.Count, Is.EqualTo(1));
             
             string comentario = "Esta reunion fue respondida";
             reunion.Comentarios.Add(comentario);
             resultado = pablo.ObtenerClientesNoRespondidos();
             Assert.That(resultado.Count, Is.EqualTo(0));
         }
       
    }
}
