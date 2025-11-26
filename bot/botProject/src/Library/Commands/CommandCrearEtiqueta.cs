
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Library;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands
{

    public class CommandCrearEtiqueta : ModuleBase<SocketCommandContext>
    {
        private readonly Fachada _fachada;

        CommandCrearEtiqueta(Fachada fachada)
        {
            _fachada = fachada;
        }

        [Command("crearEtiqueta")]
        public async Task ExecuteAsync(string nombre)
        {
            if (nombre != null)
            {
                _fachada.CrearEtiqueta(nombre);

                await ReplyAsync($"Se creo la etiqueta {nombre}");
            }
            else
            {
                await ReplyAsync($"La etiqueta debe tener nombre");
            }
        }
    }

}