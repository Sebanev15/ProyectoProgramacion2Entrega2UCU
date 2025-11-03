namespace Library;
using Library.interfaces;
public class Etiqueta
{
    public string NombreEtiqueta { get; set; }
    public List<IClienteBase> Clientes { get; set; }

    public Etiqueta(string nombreEtiqueta)
    {
        NombreEtiqueta = nombreEtiqueta;
    }
}