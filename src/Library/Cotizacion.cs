using Library.interfaces;
using System;

namespace Library
{    
    /// <summary>
    /// La clase representa una cotización hecha a un cliente.
    /// 
    /// Aplica LSP (Liskov Substitution Principle):
    ///     Cotizacion puede reemplazar a cualquier otro objeto que implemente IImporte
    ///     sin afectar el funcionamiento del sistema.
    /// 
    /// Usa el principio OCP (Open/Closed Principle) a través de IImporte:
    ///     Permite extender el sistema con nuevas clases de importes sin modificar código existente.
    /// 
    /// Aplica Polimorfismo:
    ///     Permite tratar Cotizacion y Venta de forma uniforme a través de la interfaz IImporte.
    /// 
    /// Aplica Expert y Alta Cohesion:
    ///     La clase maneja sus propios datos y se enfoca únicamente en representar una cotización.
    /// </summary>
    public class Cotizacion: IImporte
    {
        public DateTime Fecha { get; set; }
        public double Monto { get; set; }
        public Cliente Cliente { get; set; }
        
        /// <summary>
        /// Aca aplica Expert: la clase conoce y gestiona su propia información.
        /// </summary>
        
        public Cotizacion(DateTime fecha, double monto, Cliente cliente)
        {
            Fecha = fecha;
            Monto = monto;
            Cliente = cliente;
        }
    }
}
