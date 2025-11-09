using Library.interfaces;
using System.Collections.Generic;
using System;

namespace Library
{
    public class Cliente
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Genero { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public List<Etiqueta> Etiquetas { get; set; }
        public List<IInteraccion> Interacciones { get; set; }
        public List<IImporte> Importes { get; set; }

        public Cliente(string nombre, string apellido, string telefono, string correo, string genero,
            DateTime fechaDeNacimiento)
        {
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
            Correo = correo;
            Genero = genero;
            FechaDeNacimiento = fechaDeNacimiento;
            this.Etiquetas = new List<Etiqueta>();
            this.Importes = new List<IImporte>();
            this.Interacciones = new List<IInteraccion>();
        }
    }
}

