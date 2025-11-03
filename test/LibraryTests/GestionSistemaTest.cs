using Library;
using Library.abstractions;

namespace LibraryTest;
[TestFixture]
public class GestionSistemaTest
{

    private GestionClienteBase cliente;
    private GestionInteraccionBase interaccion;
    private GestionImporteBase importe;
    
    [SetUp]
    public void Setup()
    {

        cliente = new GestionCliente();
        interaccion = new GestionInteraccion();
        importe = new GestionImporte();

    }
}