using Discord;
using Discord.Interactions;
using Library;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Library.interfaces;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands
{
    public class CommandsInteraccion : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly Fachada _fachada;

        public CommandsInteraccion(Fachada fachada)
        {
            _fachada = fachada;
        }

        [SlashCommand("crearinteraccion", "Crear una Interaccion, seleccione a traves del select el tipo de interaccion")]
        public async Task AbrirModalAsync()
        {
            var modal = new ModalBuilder()
                .WithTitle("Crear nuevo Mensaje")
                .WithCustomId("modal_global:crear_Mensaje")
                .AddTextInput("Fecha", "nombre_completo", placeholder: "2000-05-01", required: true)
                .AddTextInput("Tema", "tema", placeholder: "reunion a las 4am", required: true)
                .AddTextInput("Comentarios", "comentario", placeholder: "muy tarde", required: true)
                .AddTextInput("Cliente", "cliente", placeholder: "pablo", required: true)
                .AddTextInput("Usuario", "genero", placeholder: "jorge", maxLength: 1, required: true)
                .AddTextInput("EsEnviado", "enviado", placeholder: "Si/No", required: true);

            await RespondWithModalAsync(modal.Build());
        }
        
        [SlashCommand("buscarinteraccion", "Busca interacciones por parámetros")]
        public async Task BuscarInteraccionAsync(string parametrosDeBusqueda, string clienteBusqueda)
        {
            List<string> parametros = new List<string> {};
            foreach (string parametro in parametrosDeBusqueda.Split(' '))
            {
                parametros.Add(parametro);
            }

            foreach (Cliente cliente in _fachada.GetGestionCliente().BuscarCliente(parametros))
            {
                List<IInteraccion> interacciones = _fachada.BuscarInteraccionesSinFecha(parametros, cliente);
                if (interacciones.Count == 0)
                {
                    await RespondAsync($"No se encontró ninguna interaccion con los parametros {parametrosDeBusqueda}.");
                    return;
                }

                var listaInteracciones = new StringBuilder();
                int i = 1;
                foreach (var interaccion in interacciones)
                {
                    listaInteracciones.AppendLine($"{i}. Fecha: {interaccion.Fecha.ToString()} - Tema: {interaccion.Tema} - Cliente: {interaccion.Cliente} - Usuario: {interaccion.Usuario} - Comentarios: {interaccion.Comentarios.ToString()}");
                    i++;
                }

                await RespondAsync(listaInteracciones.ToString());
            }
        }
        
    }
}