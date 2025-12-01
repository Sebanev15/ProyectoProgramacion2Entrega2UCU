using Library.interfaces;
using System.Collections.Generic;
using System;
using Ucu.Poo.DiscordBot.Domain;

namespace Library
{
    public class GestionCliente: IGestionCliente
    {
        public List<IInteraccion> Interacciones { get; set; }
        public List<IImporte>Importes { get; set; }
        public List<Cliente> Clientes { get; set; }
        
        public GestionCliente()
        {
            this.Interacciones = new List<IInteraccion>();
            this.Importes = new List<IImporte>();
            this.Clientes = new List<Cliente>();
        }

        public void RegistrarInteraccion(Cliente cliente, IInteraccion interaccion)
        {
           cliente.RegistrarInteraccion(interaccion);
            this.Interacciones.Add(interaccion);
        }

        public List<IInteraccion> BuscarInteracciones(DateTime fecha, string busqueda, Cliente cliente)
        {
            return cliente.BuscarInteracciones(fecha, busqueda);
        }

        public void AgregarComentarioInteraccion(IInteraccion interaccion, string comentario)
        {
            interaccion.AgregarComentario(comentario);
        }
        
        public List<String> ObtenerVentasTotales(DateTime fechaInicio, DateTime fechaFin)
        {
            List<String> listaVentasTotales = new List<String>();
            foreach (Cliente cliente in Clientes)
            {
                listaVentasTotales.Add(cliente.ObtenerVentasTotales(fechaInicio, fechaFin));
            }
            if (listaVentasTotales.Count == 0)
            {
                throw new ListaVaciaExcepcion("Todavia no tenes ninguna venta");
            }
            return listaVentasTotales;
        }
        
        public void AgregarImporte(IImporte importe, Cliente cliente){
            cliente.AgregarImporte(importe);
        }
    
        public void AgregarCliente (Cliente cliente){
            if (!Clientes.Contains(cliente))
            {
                this.Clientes.Add(cliente);    
            }
        }
        
        public void ModificarCliente (Cliente clienteBase, Cliente clienteModificado){ 
            clienteBase.ModificarDatos(clienteModificado);
        }

        public void EliminarCliente(Cliente cliente)
        {
            this.Clientes.Remove(cliente);
        }

        public List<Cliente> BuscarCliente(List<string> datosBusqueda)
        {
            List<Cliente> resultados = new List<Cliente>();
            
            foreach (Cliente cliente in Clientes)
            {
                foreach (var informacionAtributo in cliente.GetType().GetProperties())
                {
                    var valorAtributo = informacionAtributo.GetValue(cliente);
                    if (valorAtributo is string)
                    {
                        foreach (string datosBuscados in datosBusqueda)
                        {
                            if (valorAtributo.Equals(datosBuscados) && !resultados.Contains(cliente))
                            {
                                resultados.Add(cliente);
                            }    
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
        
        public List<Cliente> ListarClientesConReturn()
        {
            return this.Clientes;
        }

        public void AgregarEtiqueta(Cliente cliente, Etiqueta etiqueta)
        {
            cliente.AgregarEtiqueta(etiqueta);
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
            if (totalClientesInactivos.Count == 0)
            {
                throw new ListaVaciaExcepcion("No hay clientes inactivos");
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
            if (resultadoClientesNoRespondidos.Count == 0)
            {
                throw new ListaVaciaExcepcion("No hay clientes sin responder");

            }
            return resultadoClientesNoRespondidos;
        }

        public void EliminarImporte(IImporte importe)
        {
            if (Importes.Contains(importe))
            {
                this.Importes.Remove(importe);    
            }
        }

        public void ModificarImporte(IImporte importeBase, IImporte importeModificado)
        {
            importeBase.ModificarImporte(importeModificado);
        }
    }
}
