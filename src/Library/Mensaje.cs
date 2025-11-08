using Library.interfaces;
using Library.abstractions;
using System;
using System.Collections.Generic;
namespace Library
{
    public class Mensaje : IInteraccion
    {
        public DateTime Fecha { get; set; }
        public string Tema { get; set; }
        public List<string> Comentarios { get; set; }
        public IClienteBase Cliente { get; set; }
        public UsuarioBase Usuario { get; set; }

        public bool EsEnviado { get; set; }
    
        public Mensaje(DateTime fecha, string tema, IClienteBase cliente, UsuarioBase usuario, bool esEnviado)
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
