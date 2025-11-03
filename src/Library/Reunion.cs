namespace Library;
using Library.interfaces;
using Library.abstractions;

public class Reunion : IInteraccion
{
    public DateTime Fecha { get; set; }
    public string Tema { get; set; }
    public List<string> Comentarios { get; set; }
    public IClienteBase Cliente { get; set; }
    public UsuarioBase Usuario { get; set; }
    
    public string Direccion { get; set; }
    
    public Reunion(DateTime fecha, string tema, IClienteBase cliente, UsuarioBase usuario, string direccion)
    {
        this.Fecha = fecha;
        this.Tema = tema;
        this.Cliente = cliente;
        this.Usuario = usuario;
        this.Direccion = direccion;
        this.Comentarios = new List<string>();
    }
}