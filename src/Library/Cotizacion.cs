using Library.interfaces;
using System;

namespace Library
{
    public class Cotizacion: IImporte
    {
        public DateTime Fecha { get; set; }
        public double Monto { get; set; }
        public IClienteBase Cliente { get; set; }

        public Cotizacion(DateTime fecha, double monto, IClienteBase cliente)
        {
            Fecha = fecha;
            Monto = monto;
            Cliente = cliente;
        }
    }
}
