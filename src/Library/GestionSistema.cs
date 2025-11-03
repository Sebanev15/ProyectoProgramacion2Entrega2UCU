using Library.abstractions;
using Library.interfaces;
using Library.abstractions;
namespace Library;

public class GestionSistema : IGestionSistema
{
    public GestionClienteBase GestionCliente { get; set; }
    public GestionInteraccionBase GestionInteraccion { get; set; }
    public GestionImporteBase GestionImporte { get; set; }

    public GestionSistema()
    {
        GestionCliente = new GestionCliente();
        GestionImporte = new GestionImporte();
        GestionInteraccion = new GestionInteraccion();
    }
}