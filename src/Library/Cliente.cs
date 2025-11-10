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

        public void AgregarImporte(IImporte importe)
        {
            if (!Importes.Contains(importe))
            {
                this.Importes.Add(importe);
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


        public string ObtenerVentasTotales()
        {
            string nombreCliente =this.Nombre;
            double monto = 0;
            int cantidad = 0;
            
            foreach (IImporte importe in Importes)
            {
                if (importe is Venta)
                {
                    monto += importe.Monto;
                    cantidad++;
                }
            }
            string montoTotal = monto.ToString("0.0");
            string informacionVentas=$"{nombreCliente}: MontoTotal={montoTotal}, cantidad de ventas={cantidad} ";
            return informacionVentas;
        }

        public void ModificarDatos()
        {
            
        }
    }
    }

// NombreCliente las ventas totales(monto), cantidad de ventas 
