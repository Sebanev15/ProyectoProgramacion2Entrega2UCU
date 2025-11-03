using Library.abstractions;

namespace Library.interfaces;
using abstractions;
public interface IGestionSistema
{
    public GestionClienteBase GestionCliente { get; set; }
    public GestionInteraccionBase GestionInteraccion { get; set; }
    public GestionImporteBase GestionImporte { get; set; }
    
}