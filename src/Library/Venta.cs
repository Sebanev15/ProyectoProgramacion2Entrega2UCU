using Library.interfaces;
using System;

namespace Library
{
    public class Venta: IImporte
    {
        public string Producto { get; set; }
        public DateTime Fecha { get; set; }
        public double Monto { get; set; }
        public IClienteBase Cliente { get; set; }
    
        public Venta(string producto, DateTime fecha, double monto, IClienteBase cliente)
        {
            Producto = producto;
            Fecha = fecha;
            Monto = monto;
            Cliente = cliente;
        }
    }
}
