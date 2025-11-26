using Discord.WebSocket;
using Library;
using System;
using System.Linq;
using System.Threading.Tasks;
using Ucu.Poo.DiscordBot.Interfaces;

namespace Ucu.Poo.DiscordBot.Modals
{
    public class ClienteModals : IModalHandler
    {
        private readonly Fachada _fachada;

        public string CustomId => "modal_crear_cliente";

        public ClienteModals(Fachada fachada)
        {
            _fachada = fachada;
        }

        public async Task HandleAsync(SocketModal modal)
        {
            var components = modal.Data.Components.ToList();
            var nombreCompleto = components.First(x => x.CustomId == "nombre_completo").Value;
            var telefono = components.First(x => x.CustomId == "telefono").Value;
            var correo = components.First(x => x.CustomId == "correo").Value;
            var genero = components.First(x => x.CustomId == "genero").Value;
            var fechaNac = components.First(x => x.CustomId == "fecha_nac").Value;

            await ProcesarClienteAsync(modal, nombreCompleto, telefono, correo, genero, fechaNac);
        }

        private async Task ProcesarClienteAsync(
            SocketModal modal,
            string nombre_completo,
            string telefono,
            string correo,
            string genero,
            string fecha_nac)
        {
            Console.WriteLine("=== Modal procesado en ClienteModals ===");

            var partes = nombre_completo.Trim().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            if (partes.Length < 2)
            {
                await modal.RespondAsync("❌ Nombre completo inválido. Ingresa nombre y apellido separados por espacio.", ephemeral: true);
                return;
            }

            string nombre = partes[0];
            string apellido = partes[1];

            genero = genero.ToUpper();
            if (genero != "M" && genero != "H")
            {
                await modal.RespondAsync("❌ Género inválido. Debe ser M o H.", ephemeral: true);
                return;
            }

            if (!DateTime.TryParse(fecha_nac, out DateTime fechaNacimiento))
            {
                await modal.RespondAsync("❌ Fecha inválida. Usa formato AAAA-MM-DD (ejemplo: 2000-05-01).", ephemeral: true);
                return;
            }

            _fachada.CrearCliente(nombre, apellido, telefono, correo, genero, fechaNacimiento);
            await modal.RespondAsync($"✅ Cliente **{nombre} {apellido}** creado correctamente.", ephemeral: true);
        }
    }
}
