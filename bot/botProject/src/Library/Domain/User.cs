using System;
using System.Collections.Generic;
using System.Linq;

namespace Ucu.Poo.DiscordBot.Domain
{
    /// <summary>
    /// Esta clase representa un usuario de la aplicación.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Retorna el nombre del usuario en Discord.
        /// </summary>
        public string UserName { get; }

        private ICollection<Role> roles = new HashSet<Role>();

        /// <summary>
        /// Obtiene la lista de roles del usuario.
        /// </summary>
        public IReadOnlyCollection<Role> Roles
        {
            get
            {
                return this.roles.ToList().AsReadOnly();
            }
        }

        /// <summary>
        /// Crea una nueva instancia de la clase <see cref="User"/>.
        /// </summary>
        /// <param name="userName">El nombre de usuario de Discord de este
        /// usuario.</param>
        /// <exception cref="ArgumentNullException">Cuando el nombre de usuario
        /// de Discord está en blanco o es <c>null</c>.</exception>
        public User(string userName)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(userName);
            
            this.UserName = userName;
        }

        /// <summary>
        /// Determina si un usuario tiene un rol <see cref="Role"/>.
        /// </summary>
        /// <param name="role">El rol a buscar.</param>
        /// <returns><c>true</c> si el usuario tiene el rol buscado, o
        /// <c>false</c> en caso contrario.</returns>
        /// <exception cref="ArgumentNullException">Cuando el rol a buscar es
        /// <c>null</c>.</exception>
        public bool HasRole(Role role)
        {
            ArgumentNullException.ThrowIfNull(role);
            
            return this.roles.Contains(role);
        }

        /// <summary>
        /// Agrega un rol <see cref="Role"/> al usuario.
        /// </summary>
        /// <param name="role">El rol a agregar</param>
        /// <exception cref="ArgumentNullException">Cuando es rol a agregar es
        /// <c>null</c>.</exception>
        public void AddRole(Role role)
        {
            ArgumentNullException.ThrowIfNull(role);

            this.roles.Add(role);
        }

        /// <summary>
        /// Remueve el rol <see cref="Role"/> del usuario.
        /// </summary>
        /// <param name="role">El rol a remover</param>
        /// <exception cref="ArgumentNullException">Cuando el rol a remover es
        /// <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">Cuando el usuario no
        /// tiene el rol a remover.</exception>
        public void RemoveRole(Role role)
        {
            ArgumentNullException.ThrowIfNull(role);

            if (!this.roles.Contains(role))
            {
                throw new InvalidOperationException(
                    $"No se puede remover el rol {role.Name} que el usuario {this.UserName} no tiene.");
            }

            this.roles.Remove(role);
        }
    }
}
