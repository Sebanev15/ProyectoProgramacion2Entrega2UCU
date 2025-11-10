using Library.interfaces;
using System;
using System.Collections.Generic;
namespace Library
{
    /// <summary>
    /// Esta clase representa un mensaje al que se le puede asociar un cliente.
    /// Es una interacción, por lo que depende de la interfaz IInteraccion.
    /// </summary>
    public class Mensaje : IInteraccion
    {
        public DateTime Fecha { get; set; }
        public string Tema { get; set; }
        public List<string> Comentarios { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; }

        public bool EsEnviado { get; set; }
    
        public Mensaje(DateTime fecha, string tema, Cliente cliente, Usuario usuario, bool esEnviado)
        {
            this.Fecha = fecha;
            this.Tema = tema;
            this.Cliente = cliente;
            this.Usuario = usuario;
            this.EsEnviado = esEnviado;
            this.Comentarios = new List<string>();
        }
        
        public void AgregarComentario(string comentario)
        {
            this.Comentarios.Add(comentario);
        }
    }
}
