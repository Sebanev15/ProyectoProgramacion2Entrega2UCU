using Library.interfaces;
using System;

namespace Library
{
    /// <summary>
    /// La clase representa una venta realizada a un cliente.
    /// 
    /// Aplica LSP (Liskov Substitution Principle):
    ///     Puede reemplazar a cualquier objeto del tipo IImporte sin afectar el funcionamiento del sistema.
    /// 
    /// Aplica Polimorfismo:
    ///     Permite usar Venta de forma polimórfica a través de la interfaz IImporte.
    /// 
    /// Aplica alta cohesion y Expert:
    ///     Todos los atributos están directamente relacionados con una venta,
    ///     y la clase es experta en manejar su propia información.
    /// </summary>
    public class Venta : IImporte
    {
        public string Producto { get; set; }
        public DateTime Fecha { get; set; }
        public double Monto { get; set; }
        public Cliente Cliente { get; set; }

        /// <summary>
        /// Aca aplica Expert:
        ///     La clase es responsable de inicializar y conocer su propia información.
        /// </summary>
        
        public Venta(string producto, DateTime fecha, double monto, Cliente cliente)
        {
            Producto = producto;
            Fecha = fecha;
            Monto = monto;
            Cliente = cliente;
        }

        public void ModificarImporte(IImporte importeMod)
        {
            if (importeMod is Venta)
            {
                foreach (var propiedad in importeMod.GetType().GetProperties())
                {
                    var destinoProp = this.GetType().GetProperty(propiedad.Name);
                    if (destinoProp != null && destinoProp.CanWrite)
                        destinoProp.SetValue(this, propiedad.GetValue(importeMod));
                }
            }
           
        }

    }
}