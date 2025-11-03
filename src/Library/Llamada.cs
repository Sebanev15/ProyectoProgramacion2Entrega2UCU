namespace Library;
using Library.abstractions;
using Library.interfaces;

public class Llamada : IInteraccion
{
    public DateTime Fecha { get; set; }
    public string Tema { get; set; }
    public List<string> Comentarios { get; set; }
    public IClienteBase Cliente { get; set; }
    public UsuarioBase Usuario { get; set; }
    
    public Llamada(DateTime fecha, string tema, IClienteBase cliente, UsuarioBase usuario)
    {
        this.Fecha = fecha;
        this.Tema = tema;
        this.Cliente = cliente;
        this.Usuario = usuario;
        this.Comentarios = new List<string>();
    }
}