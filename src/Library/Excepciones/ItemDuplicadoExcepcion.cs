using System;

namespace Ucu.Poo.DiscordBot.Domain
{
    public class ItemDuplicadoExcepcion : Exception
    {
        public ItemDuplicadoExcepcion(string message)
            : base(message)
        {
        }
    }
}