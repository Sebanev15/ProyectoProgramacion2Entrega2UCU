using Library.interfaces;
using Library.abstractions;
using System;
using System.Collections.Generic;

namespace Library
{
    /// <summary>
    /// Esta clase representa un correo al que se le puede asociar un cliente.
    /// Es una interacción, por lo que depende de la interfaz IInteraccion.
    /// </summary>
    public class Correo : IInteraccion
    {
        public DateTime Fecha { get; set; }
        public string Tema { get; set; }
        public List<string> Comentarios { get; set; }
        public Cliente Cliente { get; set; }
        public UsuarioBase Usuario { get; set; }
    
        public bool EsEnviado { get; set; }

        public Correo(DateTime fecha, string tema, Cliente cliente, UsuarioBase usuario, bool esEnviado)
        {
            this.Fecha = fecha;
            this.Tema = tema;
            this.Cliente = cliente;
            this.Usuario = usuario;
            this.EsEnviado = esEnviado;
            this.Comentarios = new List<string>();
        }
    }
}
