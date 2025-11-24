using Library.interfaces;
using System.Collections.Generic;
using System;

namespace Library
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
    ///
    public class Cliente
    {    
        ///<summary>
        /// Nombre del Cliente
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Apellido del Cliente.
        /// </summary>
        public string Apellido { get; set; }
        /// <summary>
        /// Telefono del Cliente.
        /// </summary>
        public string Telefono { get; set; }
        /// <summary>
        /// Correo del Cliente.
        /// </summary>
        public string Correo { get; set; }
        /// <summary>
        /// Genero del Cliente.
        /// </summary>
        public string Genero { get; set; }
        /// <summary>
        /// Fecha de Nacimiento del Cliente.
        /// </summary>
        public DateTime FechaDeNacimiento { get; set; }
        /// <summary>
        /// Etiqueta/Categorizacion del Cliente.
        /// </summary>
        public List<Etiqueta> Etiquetas { get; set; }
        /// <summary>
        /// Interacciones del Cliente.
        /// </summary>
        public List<IInteraccion> Interacciones { get; set; }
        /// <summary>
        /// Importes del Cliente.
        /// </summary>
        public List<IImporte> Importes { get; set; }
        
        /// <summary>
        /// Aca aplica Creator: 
        ///     La clase Cliente es responsable de crear las listas que usará internamente.
        /// </summary>
        
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
        /// <summary>
        /// Agrega un IImporte a la lista de importes del Cliente 
        /// </summary>
        /// <param name="importe"></param> Importe a agregar.
        public void AgregarImporte(IImporte importe)
        {
            if (!Importes.Contains(importe))
            {
                this.Importes.Add(importe);
            }
        }
        /// <summary>
        /// Agrega una Etiqueta a la lista de etiquetas del Cliente.
        /// </summary>
        /// <param name="etiqueta"></param> Etiqueta a agregar.
        public void AgregarEtiqueta(Etiqueta etiqueta)
        {
            if (!Etiquetas.Contains(etiqueta))
            {
                this.Etiquetas.Add(etiqueta);
            }
        }
        /// <summary>
        /// Registra una interaccion a la lista de Interacciones del Cliente.
        /// </summary>
        /// <param name="interaccion"></param>La interaccion a registrar.
        public void RegistrarInteraccion(IInteraccion interaccion)
        {
            if (!Interacciones.Contains(interaccion))
            {
                this.Interacciones.Add(interaccion);
            }
        }
        /// <summary>
        /// Retorna una lista de interacciones, con los parametros dados.
        /// </summary>
        /// <param name="fecha"></param>Fecha en cual se buscan las interacciones.
        /// <param name="busqueda"></param>Mensaje especifico que se busca.
        /// <returns></returns>
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

        /// <summary>
        /// Obtiene las ventas generadas en cierto rango de tiempo.
        /// </summary>
        /// <param name="inicio"></param> Fecha inicial de ventas
        /// <param name="fin"></param> Fecha final de ventas.
        /// <returns></returns>
        public string ObtenerVentasTotales(DateTime inicio, DateTime fin)
        {
            string nombreCliente =this.Nombre;
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
            string informacionVentas=$"{nombreCliente}: MontoTotal={montoTotal}, cantidad de ventas={cantidad}";
            return informacionVentas;
        }
        /// <summary>
        /// Modifica los datos del Cliente, cambia por los datos de 
        /// </summary>
        /// <param name="clienteMod"></param>
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
