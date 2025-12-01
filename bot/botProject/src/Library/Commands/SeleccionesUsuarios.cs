using System.Collections.Generic;
using Library;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands
{
    public static class SeleccionesUsuarios
    {
        public static Dictionary<ulong, List<Cliente>> OpcionesClientes = new Dictionary<ulong, List<Cliente>>();
        public static Dictionary<ulong, List<Usuario>> OpcionesUsuarios = new Dictionary<ulong, List<Usuario>>();
    }
}