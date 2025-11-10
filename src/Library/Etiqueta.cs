using Library.interfaces;
using System.Collections.Generic;

namespace Library
{
    public class Etiqueta
    {    
        /// <summary>
        /// La clase representa una etiqueta que puede asociarse a varios clientes.
        /// 
        /// Aplica Alta Cohesion:
        ///     La clase se enfoca en una sola responsabilidad, manejar el nombre de la etiqueta
        ///     y la lista de clientes asociados.
        /// 
        /// Aplica Expert:
        ///     Es experta en su propia información (nombre y clientes), por lo que maneja esos datos directamente.
        /// 
        /// Aplica Bajo Acoplamiento:
        ///     Depende solo de la clase Cliente, manteniendo bajo acoplamiento y facilitando la reutilización.
        /// </summary>
        
        public string NombreEtiqueta { get; set; }
        public List<Cliente> Clientes { get; set; }

        /// <summary>
        /// Aca aplica Expert y Alta Cohesion: la clase gestiona sus propios datos.
        /// </summary>
        
        public Etiqueta(string nombreEtiqueta)
        {
            NombreEtiqueta = nombreEtiqueta;
        }
    }
}
