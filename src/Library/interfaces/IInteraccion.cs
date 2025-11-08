using Library.abstractions;
using System;
using System.Collections.Generic;

namespace Library.interfaces
{
    public interface IInteraccion
    {
        DateTime Fecha { get; set; }
        string Tema { get; set; }
        List<string> Comentarios { get; set; }
        IClienteBase Cliente { get; set; }
        UsuarioBase Usuario { get; set; }
    }

}
