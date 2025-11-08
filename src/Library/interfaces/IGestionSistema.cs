using Library.abstractions;

namespace Library.interfaces
{
    public interface IGestionSistema
    {
        GestionClienteBase GestionCliente { get; set; }
        GestionInteraccionBase GestionInteraccion { get; set; }
        GestionImporteBase GestionImporte { get; set; }
    }
}
