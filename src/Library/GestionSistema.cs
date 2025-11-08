using Library.abstractions;
using Library.interfaces;
using System.Collections.Generic;
using System;

namespace Library
{
    public class GestionSistema
    {
        public List<IInteraccion> Interacciones { get; set; }
        public List<IImporte>Importes { get; set; }
        public List<Cliente> Clientes { get; set; }
        
        public GestionSistema()
        {
            this.Interacciones = new List<IInteraccion>();
            this.Importes = new List<IImporte>();
            this.Clientes = new List<Cliente>();
        }

        public void RegistrarInteraccion(Cliente cliente, IInteraccion interaccion)
        {
            if(!cliente.Interacciones.Contains(interaccion))
            {
                cliente.Interacciones.Add(interaccion);
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

        public void AgregarComentario(IInteraccion interaccion, string comentario)
        {
            if (!interaccion.Comentarios.Contains(comentario))
            {
                interaccion.Comentarios.Add(comentario);
            }
        }
        
       

        public List<IImporte> ObtenerVentasTotales(DateTime fechaInicio, DateTime fechaFin)
        {
            List<IImporte> listaVentasTotales = new List<IImporte>();
            foreach (IImporte venta in Importes)
            {
                if (venta.Fecha>=fechaInicio && venta.Fecha<=fechaFin)
                {
                    listaVentasTotales.Add(venta);
                }
            }
            return listaVentasTotales;
        }

        public void AgregarImporte(IImporte importe, Cliente cliente){
            if (!cliente.Importes.Contains(importe))
            {
                cliente.Importes.Add(importe);    
            }
        
        }
    
        public void AgregarCliente (Cliente cliente){
            if (!Clientes.Contains(cliente))
            {
                this.Clientes.Add(cliente);    
            }
        }
        
        public void ModificarCliente (Cliente clienteBase, Cliente clienteModificado){
            if (Clientes.Contains(clienteBase))
            {
                    clienteBase.Nombre = clienteModificado.Nombre;
                    clienteBase.Apellido = clienteModificado.Apellido;
                    clienteBase.Correo = clienteModificado.Correo;
                    clienteBase.Genero = clienteModificado.Genero;
                    clienteBase.Telefono = clienteModificado.Telefono;
                    clienteBase.FechaDeNacimiento = clienteModificado.FechaDeNacimiento;
            }
        }

        public void EliminarCliente(Cliente cliente)
        {
            if (Clientes.Contains(cliente))
            {
                this.Clientes.Remove(cliente);    
            }
        }

        public List<Cliente> BuscarCliente(string clienteBusqueda)
        {
            List<Cliente> resultados = new List<Cliente>();
            
            foreach (Cliente cliente in Clientes)
            {
                foreach (var informacionAtributo in cliente.GetType().GetProperties())
                {
                    var valorAtributo = informacionAtributo.GetValue(cliente);
                    if (valorAtributo is string)
                    {
                      
                        if (valorAtributo.Equals(clienteBusqueda) && !resultados.Contains(cliente))
                        {
                            resultados.Add(cliente);
                        }
                      
                    }
                }
            }
            return resultados;
        }

        public void ListarClientes()
        {
            foreach (Cliente cliente in Clientes)
            { 
                Console.WriteLine(cliente.Nombre);
            }
        }

        public void AgregarEtiqueta(Cliente cliente, Etiqueta etiqueta)
        {
            if (!cliente.Etiquetas.Contains(etiqueta))
            {
                cliente.Etiquetas.Add(etiqueta);
            }
        }

        public List<Cliente> ObtenerClientesInactivos()
        {
            List<Cliente> totalClientesInactivos = new List<Cliente>();
            foreach (Cliente cliente in Clientes)
            {
                foreach (IInteraccion interaccion in cliente.Interacciones)
                {
                    TimeSpan rangoTiempo= DateTime.Now - interaccion.Fecha; 
                    if (rangoTiempo.TotalDays>20)
                    {
                        totalClientesInactivos.Add(cliente);
                    }
                }
            }
            return totalClientesInactivos;
        }

        public List<Cliente> ObtenerClientesNoRespondidos()
        {
            List<Cliente> resultadoClientesNoRespondidos = new List<Cliente>();
            foreach (Cliente cliente in Clientes)
            {
                foreach (IInteraccion interaccion in cliente.Interacciones)
                {
                    if (interaccion.Comentarios.Count<=0)
                    {
                        resultadoClientesNoRespondidos.Add(cliente);
                    }
                }
            }
            return resultadoClientesNoRespondidos;
        }
    }
}
