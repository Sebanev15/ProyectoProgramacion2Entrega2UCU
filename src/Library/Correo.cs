using Library.interfaces;
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
        /// <summary>
        /// Fecha del Correo.
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Tema del Correo.
        /// </summary>
        public string Tema { get; set; }
        /// <summary>
        /// Comentarios en Correo.
        /// </summary>
        public List<string> Comentarios { get; set; }
        /// <summary>
        /// Cliente que participo en Correo.
        /// </summary>
        public Cliente Cliente { get; set; }
        /// <summary>
        /// Usuario que participo en Correo.
        /// </summary>
        public Usuario Usuario { get; set; }
        
        /// <summary>
        /// Estado si la interaccion es enviada, No aplica a Reunion.
        /// </summary>
        public bool EsEnviado { get; set; }

        /// <summary>
        /// Inicializacion de Correo.
        /// </summary>
        /// <param name="fecha"></param>Fecha del Correo.
        /// <param name="tema"></param>Tema del Correo.
        /// <param name="cliente"></param>Cliente del Correo.
        /// <param name="usuario"></param>
        /// <param name="esEnviado"></param>
        public Correo(DateTime fecha, string tema, Cliente cliente, Usuario usuario, bool esEnviado)
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
