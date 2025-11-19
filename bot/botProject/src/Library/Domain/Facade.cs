using System;

namespace Ucu.Poo.DiscordBot.Domain
{
    /// <summary>
    /// Esta clase recibe las acciones y devuelve los resultados que permiten
    /// implementar las historias de usuario. Otras clases que implementan el bot
    /// usan esta <see cref="Facade"/> pero no conocen el resto de las clases del
    /// dominio. Esta clase es un singleton.
    /// </summary>
    public class Facade
    {
        #region Singleton
        
        private static Facade instance;

        // Este constructor privado impide que otras clases puedan crear instancias
        // de esta.
        private Facade()
        {
            this.usersRepository = new UsersRepository();
        }
        
        // Este constructor es interno para que en las pruebas se pueda injectar
        // un mock del repositorio de usuarios en lugar de un repositorio real.
        internal Facade(IUsersRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository);
            
            this.usersRepository = repository;
        }

        /// <summary>
        /// Obtiene la única instancia de la clase <see cref="Facade"/>.
        /// </summary>
        public static Facade Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Facade();
                }

                return instance;
            }
        }

        // /// <summary>
        // /// Inicializa este singleton. Es necesario solo en los tests.
        // /// </summary>
        // public static void Reset()
        // {
        //     instance = null;
        // }
        
        #endregion

        private IUsersRepository usersRepository;

        /// <summary>
        /// Devuelve información del usuario cuyo nombre de usuario en Discord
        /// se recibe como parámetro.
        /// </summary>
        /// <param name="userName">El nombre de usuario de Discord del usuario
        /// buscado. </param>
        /// <returns>Un texto con la información del usuario <see cref="User"/>
        /// con el nombre de usuario provisto, o texto que indica que no existe
        /// un usuario con ese nombre.
        /// </returns>
        public string GetUserInfo(string userName)
        {
            string result;

            User userFound = this.usersRepository.Find(userName);
            if (userFound == null)
            {
                result =
                    $"El usuario de Discord '{userName}' no es usuario de esta aplicación.";
            }
            else
            {
                string roles = string.Join(", ", userFound.Roles);
                result = $"El usuario '{userName}' tiene los roles " +
                         $"'{roles}' en esta aplicación.";
            }

            return result;
        }
    }
}
