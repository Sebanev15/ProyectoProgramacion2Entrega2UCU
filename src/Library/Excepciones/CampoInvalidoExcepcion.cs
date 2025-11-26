using System;

namespace Ucu.Poo.DiscordBot.Domain
{
    public class CampoInvalidoExepcion : Exception
    {
        public CampoInvalidoExepcion(string message)
            : base(message)
        {
        }
    }
}