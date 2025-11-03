namespace Library.interfaces;

public interface IImporte
{
    public DateTime Fecha { get; set; }
    public double Monto { get; set; }
    public IClienteBase Cliente{ get; set; }
}