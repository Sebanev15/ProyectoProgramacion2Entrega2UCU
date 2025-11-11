using NUnit.Framework;
using Library;
using System;
using System.IO;
using Library.interfaces;

namespace LibraryTest
{
    [TestFixture]
    public class AdministradorTest
{
    private class UsuarioGenerico : Usuario
    {
        public UsuarioGenerico(string esteNombre, string esteCorreo, string esteTelefono, IGestionCliente estaGestionCliente) : base(esteNombre, esteCorreo,
            esteTelefono, estaGestionCliente)
        {

        }
    }
    
    private Administrador administrador;
    private Library.GestionUsuario gestionUsuario;
    private UsuarioGenerico usuarioGenerico1;
    private UsuarioGenerico usuarioGenerico2;
    
    [SetUp]
    public void Setup()
    { 
        IGestionCliente gestionClienteAdmin = new GestionCliente();
        administrador = new Administrador("Sebastian","seba@gmail.com","099111222", gestionClienteAdmin);
        gestionUsuario = new Library.GestionUsuario();
        IGestionCliente gestionClienteU1 = new GestionCliente();
        IGestionCliente gestionClienteU2 = new GestionCliente();
        usuarioGenerico1 = new UsuarioGenerico("NombreGenerico", "correo@gmail.com", "099222333", gestionClienteU1);
        usuarioGenerico2 = new UsuarioGenerico("NombreGenerico", "correo2@gmail.com", "099333444", gestionClienteU2);
        gestionUsuario.Usuarios.Add(usuarioGenerico1);
    }
    [Test]
    public void ConstructorTest()
    {
        Assert.That(administrador.Nombre, Is.EqualTo("Sebastian"));
        Assert.That(administrador.Correo, Is.EqualTo("seba@gmail.com"));
        Assert.That(administrador.Telefono, Is.EqualTo("099111222"));
    }
    
    [Test]
    public void CrearUsuarioTest()
    {
        administrador.CrearUsuario(usuarioGenerico2, gestionUsuario);
        Assert.That(gestionUsuario.Usuarios.Count, Is.EqualTo(2));
        Assert.That(gestionUsuario.Usuarios[1], Is.EqualTo(usuarioGenerico2));
    }

    [Test]
    public void CrearUsuarioYaExistenteTest()
    {
        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);
        
        administrador.CrearUsuario(usuarioGenerico1, gestionUsuario);
        string output = consoleOutput.ToString();
        Assert.That(output.Contains("ERROR: No se pudo añadir el usuario al Cliente"));
    }

    [Test]
    public void SuspenderUsuarioTest()
    {
        Assert.That(!usuarioGenerico1.EstaSuspendido);
        administrador.SuspenderUsuario(usuarioGenerico1);
        Assert.That(usuarioGenerico1.EstaSuspendido);
    }

    [Test]
    public void EliminarUsuarioTest()
    {
        administrador.EliminarUsuario(usuarioGenerico1, gestionUsuario);
        Assert.That(gestionUsuario.Usuarios.Count, Is.EqualTo(0));
    }

    [Test]
    public void EliminarUsuarioNoExistenteTest()
    {
        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);
        administrador.EliminarUsuario(usuarioGenerico2, gestionUsuario);
        string output = consoleOutput.ToString();
        Assert.That(output.Contains("ERROR: No se pudo eliminar el usuario porque no estaba en el Cliente"));
    }
}
}
