using System;

namespace Library.interfaces
{
    public interface IImporte
    {
        DateTime Fecha { get; set; }
        double Monto { get; set; }
        Cliente Cliente{ get; set; }

        public void ModificarImporte(IImporte importeModificado)
        {
        }

    }
}
