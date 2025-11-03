namespace LibraryTest;
using Library;

[TestFixture]
public class UsuarioTest
{
    private Usuario usuario;
    [SetUp]
    public void Setup()
    { 
        usuario = new Usuario("Sebastian","seba@gmail.com","099111222");
    }
    [Test]
    public void ConstructorTest()
    {
        
        Assert.That(usuario.Nombre, Is.EqualTo("Sebastian"));
        Assert.That(usuario.Correo, Is.EqualTo("seba@gmail.com"));
        Assert.That(usuario.Telefono, Is.EqualTo("099111222"));
    }

    [Test]
    public void SuspenderTest()
    {
        usuario.Suspender();
        Assert.IsTrue(usuario.EstaSuspendido);
    }

    [Test]
    public void ReactivarTest()
    {
        usuario.Suspender();
        usuario.Reactivar();
        Assert.IsFalse(usuario.EstaSuspendido);
    }
    
}
    