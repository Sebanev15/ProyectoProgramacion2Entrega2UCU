namespace Library.interfaces;
using Library.abstractions;

public interface IInteraccion
{
    public DateTime Fecha { get; set; }
    public string Tema { get; set; }
    public List<string> Comentarios { get; set; }
    public IClienteBase Cliente { get; set; }
    public UsuarioBase Usuario { get; set; }
}
