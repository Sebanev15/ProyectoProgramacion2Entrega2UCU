using Library.interfaces;
using System;

namespace Library
{
    public class Cotizacion: IImporte
    {
        public DateTime Fecha { get; set; }
        public double Monto { get; set; }
        public Cliente Cliente { get; set; }

        public Cotizacion(DateTime fecha, double monto, Cliente cliente)
        {
            Fecha = fecha;
            Monto = monto;
            Cliente = cliente;
        }
    }
}
