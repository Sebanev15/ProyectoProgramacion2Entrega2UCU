using Library.interfaces;
using NUnit.Framework;

namespace LibraryTest;
using Library;

public class ClienteTests
{
    private Cliente j;
    [SetUp]
    public void Setup()
    {
        j = new Cliente("Juan", "Sanchez", "099477123", "correo@mail.com", "Masculino", new DateTime(1997, 10, 24));
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
        
        Assert.That(j.Etiquetas.Count(), Is.EqualTo(1));
        Assert.That(j.Etiquetas[0].NombreEtiqueta, Is.EqualTo("VIP"));
        Assert.That(j.Interacciones, Is.EqualTo(interacciones));
        Assert.That(j.Importes, Is.EqualTo(importes));
    }
}