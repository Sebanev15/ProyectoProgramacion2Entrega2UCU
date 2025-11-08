using System;

namespace Library.interfaces
{
    public interface IImporte
    {
        DateTime Fecha { get; set; }
        double Monto { get; set; }
        IClienteBase Cliente{ get; set; }
    }
}
