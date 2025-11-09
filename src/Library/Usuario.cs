using Library.abstractions;
using Library.interfaces;

namespace Library
{
    public class Usuario: UsuarioBase
    {
        /// <summary>
        /// Representa un usuario base sin privilegios especiales.
        /// </summary>
        /// <remarks>
        /// **SOLID: Liskov Substitution Principle (LSP):**  Puede sustituir a 'UsuarioBase' en cualquier contexto sin alterar el comportamiento esperado, ya que no modifica ni restringe el contrato del tipo base.
        /// </remarks>
        public Usuario(string esteNombre, string esteCorreo, string esteTelefono, IGestionSistema estaGestionSistema) : base(esteNombre, esteCorreo,
            esteTelefono, estaGestionSistema)
        {

        }
    }
}
