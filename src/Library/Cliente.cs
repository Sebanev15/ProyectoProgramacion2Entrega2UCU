using Library.interfaces;
using System.Collections.Generic;
using System;
using System.Threading;
using Ucu.Poo.DiscordBot.Domain;

namespace Library
{
    public class Cliente
    {    
        /// <summary>
        /// La clase representa a un cliente del sistema.
        /// 
        /// Aplica SRP (Single Responsibility Principle):
        ///     Tiene una única responsabilidad (modelar los datos y relaciones de un cliente).
        /// 
        /// Aplica alta cohesion y bajo acoplamiento:
        ///     Todos los atributos se relacionan directamente con el cliente (alta cohesión),
        ///     y depende de interfaces en lugar de clases concretas (bajo acoplamiento).
        /// 
        /// Aplica DIP (Dependency Inversion Principle):
        ///     Depende de las abstracciones IInteraccion e IImporte, no de implementaciones concretas.
        /// 
        /// Aplica Expert y Creator:
        ///     Es la experta en su propia información y crea internamente las listas que gestiona.
        /// </summary>
        
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Genero { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public List<Etiqueta> Etiquetas { get; set; }
        public List<IInteraccion> Interacciones { get; set; }
        public List<IImporte> Importes { get; set; }
        
        /// <summary>
        /// Aca aplica Creator: 
        ///     La clase Cliente es responsable de crear las listas que usará internamente.
        /// </summary>
        
        public Cliente(string nombre, string apellido, string telefono, string correo, string genero,
            DateTime fechaDeNacimiento)
        {            
            if (nombre != null && apellido != null && telefono != null && correo != null && genero != null &&
                         fechaDeNacimiento != null)
            {
                if (genero.ToUpper() == "M" || genero.ToUpper() == "H")
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
                else
                {
                    throw new CampoInvalidoExepcion("El genero debe ser M para mujer o H para hombre.");
                }
            }
            else
            {
                throw new CampoInvalidoExepcion("No pueden haber campos vacios");
            } 

        }

        public void AgregarImporte(IImporte importe)
        {
            if (!Importes.Contains(importe))
            {
                if (importe is Venta)
                {
                    Venta v = (Venta)importe;
                    Venta importeNuevo = new Venta(v.Producto, v.Fecha, v.Monto, this);
                    Importes.Add(importeNuevo);
                }
                else if (importe is Cotizacion)
                {
                    Cotizacion c = (Cotizacion)importe;
                    Cotizacion importeNuevo = new Cotizacion(c.Fecha, c.Monto, this);
                    Importes.Add(importeNuevo);
                }
            }
        }


        
        public void AgregarEtiqueta(Etiqueta etiqueta)
        {
            if (!Etiquetas.Contains(etiqueta))
            {
                this.Etiquetas.Add(etiqueta);
            }
        }
        public void RegistrarInteraccion(IInteraccion interaccion)
        {
            if (!Interacciones.Contains(interaccion))
            {
                this.Interacciones.Add(interaccion);
            }
        }
        
        public List<IInteraccion> BuscarInteracciones(DateTime fecha, string busqueda)
        {
            List<IInteraccion> resultadoInteracciones = new List<IInteraccion>();
            foreach (IInteraccion interaccion in Interacciones)
            {
                foreach (var informacionAtributo in interaccion.GetType().GetProperties())
                {
                    var valorAtributo = informacionAtributo.GetValue(interaccion);
                    if (valorAtributo is string)
                    {
                        if (valorAtributo.Equals(busqueda) && !resultadoInteracciones.Contains(interaccion))
                        {
                            if (interaccion.Fecha==fecha)
                            {
                                resultadoInteracciones.Add(interaccion);
                            }
                        }
                    }
                }
            }
            return resultadoInteracciones;
        }


        public string ObtenerVentasTotales(DateTime inicio, DateTime fin)
        {
            string nombreCliente =this.Nombre + " " + this.Apellido;
            double monto = 0;
            int cantidad = 0;
            
            foreach (IImporte importe in Importes)
            {
                if (importe is Venta && (importe.Fecha >=inicio && importe.Fecha <=fin)  )
                {
                    monto += importe.Monto;
                    cantidad++;
                }
            }
            string montoTotal = monto.ToString("0.0");
            string informacionVentas=$"{nombreCliente}: MontoTotal = {montoTotal}, cantidad de ventas = {cantidad}";
            return informacionVentas;
        }

        public void ModificarDatos(Cliente clienteMod)
        { 
            foreach (var propiedad in clienteMod.GetType().GetProperties())
                {
                    var destinoProp = this.GetType().GetProperty(propiedad.Name);
                    if (destinoProp != null && destinoProp.CanWrite)
                        destinoProp.SetValue(this, propiedad.GetValue(clienteMod));
                }
            }   
        }
    }
    

// NombreCliente las ventas totales(monto), cantidad de ventas 
