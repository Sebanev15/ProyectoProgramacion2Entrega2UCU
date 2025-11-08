using Library.interfaces;
using System.Collections.Generic;
using System;

namespace Library.abstractions
{
    public abstract class GestionClienteBase
{
    public List<IClienteBase> Clientes { get; set; }

    public GestionClienteBase()
    {
        this.Clientes = new List<IClienteBase>();
    }

    public void AgregarCliente (IClienteBase cliente){
        if (!Clientes.Contains(cliente))
        {
            this.Clientes.Add(cliente);    
        }
    }
    
    public void ModificarCliente (IClienteBase clienteBase, IClienteBase clienteModificado){
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

    public void EliminarCliente(IClienteBase cliente)
    {
        if (Clientes.Contains(cliente))
        {
            this.Clientes.Remove(cliente);    
        }
        else
        {
            
        }


    }

    public List<IClienteBase> BuscarCliente(string clienteBusqueda)
    {
        List<IClienteBase> resultados = new List<IClienteBase>();
        
        foreach (IClienteBase cliente in Clientes)
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
        foreach (IClienteBase cliente in Clientes)
        { 
            Console.WriteLine(cliente.Nombre);
        }
    }

    public void AgregarEtiqueta(IClienteBase cliente, Etiqueta etiqueta)
    {
        if (!cliente.Etiquetas.Contains(etiqueta))
        {
            cliente.Etiquetas.Add(etiqueta);
        }
    }

    public List<IClienteBase> ObtenerClientesInactivos()
    {
        List<IClienteBase> totalClientesInactivos = new List<IClienteBase>();
        foreach (IClienteBase cliente in Clientes)
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

    public List<IClienteBase> ObtenerClientesNoRespondidos()
    {
        List<IClienteBase> resultadoClientesNoRespondidos = new List<IClienteBase>();
        foreach (IClienteBase cliente in Clientes)
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

