using System;

namespace Ucu.Poo.DiscordBot.Domain
{
    public class ListaVaciaExcepcion : Exception
    {
        public ListaVaciaExcepcion(string message)
            : base(message)
        {
        }
    }
}