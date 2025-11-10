using Library.interfaces;
using Library.abstractions;
using System;
using System.Collections.Generic;

namespace Library
{
    /// <summary>
    /// Esta clase representa una reunión a la que se le puede asociar un cliente.
    /// Es una interacción, por lo que depende de la interfaz IInteraccion.
    /// </summary>
    public class Reunion : IInteraccion
    {
        public DateTime Fecha { get; set; }
        public string Tema { get; set; }
        public List<string> Comentarios { get; set; }
        public Cliente Cliente { get; set; }
        public UsuarioBase Usuario { get; set; }
    
        public string Direccion { get; set; }
    
        public Reunion(DateTime fecha, string tema, Cliente cliente, UsuarioBase usuario, string direccion)
        {
            this.Fecha = fecha;
            this.Tema = tema;
            this.Cliente = cliente;
            this.Usuario = usuario;
            this.Direccion = direccion;
            this.Comentarios = new List<string>();
        }
    }
}
