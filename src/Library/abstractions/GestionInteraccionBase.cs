using Library.interfaces;
using System.Collections.Generic;
using System;
namespace Library.abstractions
{
    public class GestionInteraccionBase
    {
        public List<IInteraccion> Interacciones { get; set; }

        public GestionInteraccionBase()
        {
            this.Interacciones = new List<IInteraccion>();
        }

        public void RegistrarInteraccion(IClienteBase cliente, IInteraccion interaccion)
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

    }
}