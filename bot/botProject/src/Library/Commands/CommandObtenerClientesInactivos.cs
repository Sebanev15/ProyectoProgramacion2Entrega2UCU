using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands
{
    public class CommandObtenerClientesInactivos : ModuleBase<SocketCommandContext>
    {
        private readonly Fachada _fachada;

        public CommandObtenerClientesInactivos(Fachada fachada)
        {
            _fachada = fachada;
        }

        [Command("obtenerClientesInactivos")]
        public async Task ExecuteAsync()
        {
            try
            {
                List<Cliente> resultado = _fachada.ObtenerClientesInactivos();

                string mensaje = "Clientes inactivos:\n" + string.Join("\n", resultado.Select(c => $"- {c.Nombre} {c.Apellido}"));

                await ReplyAsync(mensaje);
            }
            catch (ListaVaciaExcepcion e)
            {
                await ReplyAsync(e.Message);
            }
        }
    }
}