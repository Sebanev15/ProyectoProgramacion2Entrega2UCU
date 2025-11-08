using Library.interfaces;
using System.Collections.Generic;

namespace Library
{
    public class Etiqueta
    {
        public string NombreEtiqueta { get; set; }
        public List<IClienteBase> Clientes { get; set; }

        public Etiqueta(string nombreEtiqueta)
        {
            NombreEtiqueta = nombreEtiqueta;
        }
    }
}
