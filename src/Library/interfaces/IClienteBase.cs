namespace Library.interfaces;

public interface IClienteBase
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Telefono { get; set; }
    public string Correo { get; set; }
    public string Genero { get; set; }
    public DateTime FechaDeNacimiento { get; set; }
    public List<Etiqueta> Etiquetas { get; set; }
    public List<IInteraccion> Interacciones { get; set; }
    public List<IImporte> Importes { get; set; }
}