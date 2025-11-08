using Library.abstractions;

namespace Library
{
    public class Usuario: UsuarioBase
    {
        public Usuario(string esteNombre, string esteCorreo, string esteTelefono) : base(esteNombre, esteCorreo,
            esteTelefono)
        {

        }
    }
}
