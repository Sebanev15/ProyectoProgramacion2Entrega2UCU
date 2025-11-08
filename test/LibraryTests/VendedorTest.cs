using Library.interfaces;
using Library;
using System.Collections.Generic;
using System;
using System.IO;
using NUnit.Framework;

namespace LibraryTest
{
    public class VendedorTest
{
    private class ClienteGenerico : IClienteBase
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Genero { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public List<Etiqueta> Etiquetas { get; set; }
        public List<IInteraccion> Interacciones { get; set; }
        public List<IImporte> Importes { get; set; }

        public ClienteGenerico(string nombre, string apellido, string telefono, string correo, string genero,
            DateTime fechaDeNacimiento)
        {
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
            Correo = correo;
            Genero = genero;
            FechaDeNacimiento = fechaDeNacimiento;
            Importes = new List<IImporte>();
            Interacciones = new List<IInteraccion>();
            Etiquetas = new List<Etiqueta>();
        }
    }
    private ClienteGenerico cliente;
    private Vendedor vendedor1;
    private Vendedor vendedor2;
    [SetUp]
    public void Setup()
    { 
        vendedor1 = new Vendedor("Sebastian","seba@gmail.com","099111222");
        vendedor2 = new Vendedor("Jose", "jose@gmail.com", "099000111");
        DateTime diaNacimiento = new DateTime(2000, 1, 1);
        cliente = new ClienteGenerico("Pepe", "Rodriguez", "091222333", "pepe@gmail.com", "masculino", diaNacimiento);
        vendedor1.GestionSistema.GestionCliente.AgregarCliente(cliente);
    }
    
    [Test]
    public void ConstructorTest()
    {
        
        Assert.That(vendedor1.Nombre, Is.EqualTo("Sebastian"));
        Assert.That(vendedor1.Correo, Is.EqualTo("seba@gmail.com"));
        Assert.That(vendedor1.Telefono, Is.EqualTo("099111222"));
    }

    [Test]
    public void AsignarOtroVendedorCorrectoTest()
    {
        vendedor1.AsignarOtroVendedor(vendedor2, cliente);
        Assert.That(vendedor1.GestionSistema.GestionCliente.Clientes.Count, Is.EqualTo(0));
        Assert.That(vendedor2.GestionSistema.GestionCliente.Clientes.Count, Is.EqualTo(1));
        Assert.That(vendedor2.GestionSistema.GestionCliente.Clientes.Contains(cliente));
    }

    [Test]
    public void AsignarOtroVendedorIncorrectoTest()
    {
        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);
        
        vendedor2.AsignarOtroVendedor(vendedor1, cliente);
        string output = consoleOutput.ToString();
        
        Assert.That(output.Contains("ERROR: El cliente no existe"));
        Assert.That(vendedor2.GestionSistema.GestionCliente.Clientes.Count, Is.EqualTo(0));

    }
}
}
