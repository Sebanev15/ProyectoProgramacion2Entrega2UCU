using System;
using System.Collections.Generic;

namespace Library.interfaces
{
    public interface IClienteBase
    {
        string Nombre { get; set; }
        string Apellido { get; set; }
        string Telefono { get; set; }
        string Correo { get; set; }
        string Genero { get; set; }
        DateTime FechaDeNacimiento { get; set; }
        List<Etiqueta> Etiquetas { get; set; }
        List<IInteraccion> Interacciones { get; set; }
        List<IImporte> Importes { get; set; }
    }
}

