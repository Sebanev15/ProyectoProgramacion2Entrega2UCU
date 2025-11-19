using System;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Ucu.Poo.DiscordBot.Services;


namespace Ucu.Poo.DiscordBot.Domain
{
    /// <summary>
    /// Un tipo de repositorio de usuarios que se implementa con
    /// <see cref="Ucu.Poo.DiscordBot.Domain.UsersRepository"/> en producción
    /// y por un mock en las pruebas.
    /// </summary>
    public interface IUsersRepository
    {
        /// <summary>
        /// Busca un usuario <see cref="User"/> en el repositorio que tenga
        /// el nombre de usuario de Discord provisto..
        /// </summary>
        /// <param name="userName">El nombre de usuario de Discord a
        /// buscar.</param>
        /// <returns>El usuario encontrado, si lo hubiera;<c>null</c> en caso
        /// contrario.</returns>
        User Find(string userName);

        /// <summary>
        /// Obtiene una colección con todos los usuarios <see cref="User"/>.
        /// </summary>
        IReadOnlyCollection<User> AllUsers { get; }
    }

    /// <summary>
    /// Un repositorio de usuarios <see cref="User"/>.
    /// </summary>
    public class UsersRepository : IUsersRepository
    {
        private void AddFirstAdmin()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .BuildServiceProvider();

            string firstAdminName = configuration["FirstAdmin"];
            if (string.IsNullOrEmpty(firstAdminName))
            {
                throw new InvalidOperationException("Falta el primer administrador.");
            }

            User firstAdmin = new User(firstAdminName);
            firstAdmin.AddRole(Role.Admin);
            firstAdmin.AddRole(Role.User);
            this.users.Add(firstAdmin);
        }

        /// <summary>
        /// Inicializa una nueva instancia de esta clase. Agrega el primer
        /// <see cref="User"/> que es un administrador, para que luego ese
        /// usuario pueda agregar a los demás usuarios. El nombre de usuario de
        /// este administrador se obtiene junto con el token del bot en
        /// <see cref="Bot.StartAsync(ServiceProvider)"/>.
        /// </summary>
        public UsersRepository()
        {
            this.AddFirstAdmin();
        }

        private List<User> users = new List<User>();

        /// <inheritdoc/>
        public User Find(string userName)
        {
            return this.users.Find(u => u.UserName == userName);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IReadOnlyCollection<User> AllUsers
        {
            get { return this.users.AsReadOnly();  }
        }
    }
}
