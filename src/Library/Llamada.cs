using Library.abstractions;
using Library.interfaces;
using System;
using System.Collections.Generic;

namespace Library
{
    /// <summary>
    /// Esta clase representa una llamada a la que se le puede asociar un cliente.
    /// Es una interacción, por lo que depende de la interfaz IInteraccion.
    /// </summary>
    public class Llamada : IInteraccion
    {
        public DateTime Fecha { get; set; }
        public string Tema { get; set; }
        public List<string> Comentarios { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; }
    
        public Llamada(DateTime fecha, string tema, Cliente cliente, Usuario usuario)
        {
            this.Fecha = fecha;
            this.Tema = tema;
            this.Cliente = cliente;
            this.Usuario = usuario;
            this.Comentarios = new List<string>();
        }
    }
}
