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
        public UsuarioGenerico(string esteNombre, string esteCorreo, string esteTelefono, IGestionUsuario estaGestionUsuario, IGestionCliente estaGestionCliente) : base(esteNombre, esteCorreo,
            esteTelefono, estaGestionUsuario, estaGestionCliente)
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
        IGestionUsuario gestionClienteAdmin = new GestionUsuario();
        administrador = new Administrador("Sebastian","seba@gmail.com","099111222", gestionClienteAdmin,new GestionCliente());
        gestionUsuario = new GestionUsuario();
        usuarioGenerico1 = new UsuarioGenerico("NombreGenerico", "correo@gmail.com", "099222333", new GestionUsuario(), new GestionCliente());
        usuarioGenerico2 = new UsuarioGenerico("NombreGenerico", "correo2@gmail.com", "099333444", new GestionUsuario(), new GestionCliente());
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
        administrador.CrearUsuario(usuarioGenerico1, gestionUsuario);
        Assert.That(administrador.GestionUsuario.Usuarios.Count, Is.EqualTo(0));
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
    public void ReactivarUsuarioTest()
    {
        administrador.SuspenderUsuario(usuarioGenerico1);
        Assert.That(usuarioGenerico1.EstaSuspendido);
        administrador.ReactivarUsuario(usuarioGenerico1);
        Assert.That(usuarioGenerico1.EstaSuspendido, Is.False);
        
    }
}
}
