using Library.abstractions;
using System;
using System.Collections.Generic;

namespace Library.interfaces
{
    /// <summary>
    /// Esta interfaz define el contrato para los tipos de interacciones que puede tener un cliente
    /// 
    /// Cumple con ISP (Interface Segregation Principle): Presenta únicamente las propiedades necesarias y que todas las
    /// interacciones comparten.
    /// Cumple con DIP (Dependency Inversion Principle): Permite que las clases dependan de la abstracción IInteraccion en
    /// lugar de clases concretas (como Mensaje o Correo).
    /// Cumple con OCP (Open/Closed Principle): Se pueden agregar nuevas clases que implementen IInteraccion sin modificar
    /// esta interfaz ni alterar las clases que ya dependen de ésta.
    /// Aplica Polimorfismo: Las clases que dependen de IInterfaz permiten llamar a sus métodos en común, es decir,
    /// con la misma firma, de forma polimórfica (pueden ejecutarse todos con una sola instrucción).
    /// Bajo acoplamiento: Disminuye las dependencias entre clases y el código repetido utilizando una abstracción.
    /// 
    /// </summary>
    public interface IInteraccion
    {
        DateTime Fecha { get; set; }
        string Tema { get; set; }
        List<string> Comentarios { get; set; }
        Cliente Cliente { get; set; }
        Usuario Usuario { get; set; }
    }

  

}
