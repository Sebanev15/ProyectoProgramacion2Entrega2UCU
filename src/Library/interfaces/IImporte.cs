using System;

namespace Library.interfaces
{
    /// <summary>
    /// Esta interfaz define el contrato para cualquier tipo de importe del sistema.
    /// 
    /// Aplica ISP (Interface Segregation Principle):
    ///     Expone solo las propiedades esenciales que todas las clases de importe deben tener.
    /// 
    /// Aplica DIP (Dependency Inversion Principle):
    ///     Permite que las clases dependan de la abstracción IImporte en lugar de clases concretas (como Venta o Cotizacion).
    /// 
    /// Aplica OCP (Open/Closed Principle):
    ///     Se pueden agregar nuevas clases que implementen IImporte sin modificar esta interfaz.
    /// 
    /// Aplica bajo acoplamiento:
    ///     Disminuye el acoplamiento entre las clases del dominio al utilizar una interfaz común.
    /// </summary>
    
    public interface IImporte
    {
        DateTime Fecha { get; set; }
        double Monto { get; set; }
        Cliente Cliente { get; set; }

        void ModificarImporte(IImporte importemMod);



    }
}