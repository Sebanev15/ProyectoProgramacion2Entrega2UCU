using Library.interfaces;
using System.Collections.Generic;
using System;
namespace Library.abstractions
{
    public abstract class GestionImporteBase
    {
        public List<IImporte>Importes { get; set; }


        public GestionImporteBase()
        {
            this.Importes = new List<IImporte>();
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

        public void AgregarImporte(IImporte importe, IClienteBase cliente){
            if (!cliente.Importes.Contains(importe))
            {
                cliente.Importes.Add(importe);    
            }
        
        }

    }
}