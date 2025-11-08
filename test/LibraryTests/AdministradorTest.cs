using NUnit.Framework;
using Library;
using System;
using System.IO;
using Library.abstractions;

namespace LibraryTest
{
    [TestFixture]
    public class AdministradorTest
{
    private class UsuarioGenerico : UsuarioBase
    {
        public UsuarioGenerico(string esteNombre, string esteCorreo, string esteTelefono) : base(esteNombre, esteCorreo,
            esteTelefono)
        {

        }
    }
    
    private Administrador administrador;
    private Library.System sistema;
    private UsuarioGenerico usuarioGenerico1;
    private UsuarioGenerico usuarioGenerico2;
    
    [SetUp]
    public void Setup()
    { 
        administrador = new Administrador("Sebastian","seba@gmail.com","099111222");
        sistema = new Library.System();
        usuarioGenerico1 = new UsuarioGenerico("NombreGenerico", "correo@gmail.com", "099222333");
        usuarioGenerico2 = new UsuarioGenerico("NombreGenerico", "correo2@gmail.com", "099333444");
        sistema.usuarios.Add(usuarioGenerico1);
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
        administrador.CrearUsuario(usuarioGenerico2, sistema);
        Assert.That(sistema.usuarios.Count, Is.EqualTo(2));
        Assert.That(sistema.usuarios[1], Is.EqualTo(usuarioGenerico2));
    }

    [Test]
    public void CrearUsuarioYaExistenteTest()
    {
        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);
        
        administrador.CrearUsuario(usuarioGenerico1, sistema);
        string output = consoleOutput.ToString();
        Assert.That(output.Contains("ERROR: No se pudo añadir el usuario al sistema"));
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
        administrador.EliminarUsuario(usuarioGenerico1, sistema);
        Assert.That(sistema.usuarios.Count, Is.EqualTo(0));
    }

    [Test]
    public void EliminarUsuarioNoExistenteTest()
    {
        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);
        administrador.EliminarUsuario(usuarioGenerico2, sistema);
        string output = consoleOutput.ToString();
        Assert.That(output.Contains("ERROR: No se pudo eliminar el usuario porque no estaba en el sistema"));
    }
}
}
