using System;

namespace Ucu.Poo.DiscordBot.Domain
{
    /// <summary>
    /// Esta clase representa un rol en la aplicación.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Obtiene el nombre del rol.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Role"/> con
        /// el nombre que se recibe como argumento.
        /// </summary>
        /// <param name="name">El nombre del rol.</param>
        /// <exception cref="ArgumentNullException">Cuando no se provee un nombre
        /// al crear el rol.</exception>
        public Role(string name)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(name);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);

            this.Name = name;
        }

        /// <summary>
        /// Un rol de administrador. Aunque nada previere que pueda haber varios
        /// roles de administrador, este rol se provee para facilitar el uso de
        /// los roles comunes.
        /// </summary>
        public static Role Admin { get; } = new Role("Admin");

        /// <summary>
        /// Un rol de usuario. Aunque nada previere que pueda haber varios
        /// roles de usuario, este rol se provee para facilitar el uso de los
        /// roles comunes.
        /// </summary>
        public static Role User { get; } = new Role("User");

        /// <summary>
        /// Retorna una representación del rol, que consiste en su nombre.
        /// </summary>
        /// <returns>El nombre del rol.</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
