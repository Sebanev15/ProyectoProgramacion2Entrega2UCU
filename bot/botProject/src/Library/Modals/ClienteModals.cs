using Discord;
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

        public string CustomId => "modal_global:*";

        public ClienteModals(Fachada fachada)
        {
            _fachada = fachada;
        }

        public async Task HandleAsync(SocketModal modal)
        {
            await modal.DeferAsync(ephemeral: true);

            string cid = modal.Data.CustomId ?? string.Empty;

            var components = modal.Data.Components.ToList();
            var nombreCompleto = components.FirstOrDefault(x => x.CustomId == "nombre_completo")?.Value;
            var telefono = components.FirstOrDefault(x => x.CustomId == "telefono")?.Value;
            var correo = components.FirstOrDefault(x => x.CustomId == "correo")?.Value;
            var genero = components.FirstOrDefault(x => x.CustomId == "genero")?.Value;
            var fechaNac = components.FirstOrDefault(x => x.CustomId == "fecha_nac")?.Value;

            if (cid == "modal_global:crear_cliente")
            {
                await ProcesarCreacionAsync(modal, nombreCompleto, telefono, correo, genero, fechaNac);
            }
            else if (cid.StartsWith("modal_global:modificar_cliente", StringComparison.OrdinalIgnoreCase))
            {
                await ProcesarModificacionAsync(modal, nombreCompleto, telefono, correo, genero, fechaNac);
            }
        }


        private async Task ProcesarCreacionAsync(
            SocketModal modal,
            string nombre_completo,
            string telefono,
            string correo,
            string genero,
            string fecha_nac)
        {
            var partes = nombre_completo.Trim().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            if (partes.Length < 2)
            {
                await modal.FollowupAsync("❌ Nombre completo inválido. Ingresa nombre y apellido separados por espacio.", ephemeral: true);
                return;
            }

            string nombre = partes[0];
            string apellido = partes[1];

            genero = (genero ?? string.Empty).ToUpper();
            if (genero != "M" && genero != "H")
            {
                await modal.FollowupAsync("❌ Género inválido. Debe ser M o H.", ephemeral: true);
                return;
            }

            if (!DateTime.TryParse(fecha_nac, out DateTime fechaNacimiento))
            {
                await modal.FollowupAsync("❌ Fecha inválida. Usa formato AAAA-MM-DD (ejemplo: 2000-05-01).", ephemeral: true);
                return;
            }

            Cliente cliente = _fachada.CrearCliente(nombre, apellido, telefono, correo, genero, fechaNacimiento);
            _fachada.AgregarCliente(cliente);

            await modal.FollowupAsync($"✅ Cliente **{nombre} {apellido}** creado correctamente.", ephemeral: true);
        }

        private async Task ProcesarModificacionAsync(
            SocketModal modal,
            string nombre_completo,
            string telefono,
            string correo,
            string genero,
            string fecha_nac)
        {
            var partes = nombre_completo?.Trim().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            if (partes == null || partes.Length < 1)
            {
                await modal.FollowupAsync("❌ Nombre inválido.", ephemeral: true);
                return;
            }

            string nombre = partes[0];
            string apellido = partes.Length > 1 ? partes[1] : string.Empty;

            genero = (genero ?? string.Empty).ToUpper();
            if (!string.IsNullOrEmpty(genero) && genero != "M" && genero != "H")
            {
                await modal.FollowupAsync("❌ Género inválido. Debe ser M o H.", ephemeral: true);
                return;
            }

            DateTime fechaNacimiento = DateTime.MinValue;
            if (!string.IsNullOrWhiteSpace(fecha_nac) && !DateTime.TryParse(fecha_nac, out fechaNacimiento))
            {
                await modal.FollowupAsync("❌ Fecha inválida. Usa formato AAAA-MM-DD.", ephemeral: true);
                return;
            }

            var clientes = _fachada.ListarClientesConReturn();
            Cliente clienteBase = clientes.FirstOrDefault(c =>
                string.Equals($"{c.Nombre} {c.Apellido}".Trim(), nombre_completo.Trim(), StringComparison.OrdinalIgnoreCase)
                || string.Equals(c.Nombre, nombre, StringComparison.OrdinalIgnoreCase));

            if (clienteBase == null)
            {
                await modal.FollowupAsync("⚠️ Cliente no encontrado. No se puede modificar.", ephemeral: true);
                return;
            }

            Cliente clienteModificado = _fachada.CrearCliente(
                string.IsNullOrWhiteSpace(nombre) ? clienteBase.Nombre : nombre,
                string.IsNullOrWhiteSpace(apellido) ? clienteBase.Apellido : apellido,
                string.IsNullOrWhiteSpace(telefono) ? clienteBase.Telefono : telefono,
                string.IsNullOrWhiteSpace(correo) ? clienteBase.Correo : correo,
                string.IsNullOrWhiteSpace(genero) ? clienteBase.Genero : genero,
                fechaNacimiento == DateTime.MinValue ? clienteBase.FechaDeNacimiento : fechaNacimiento
            );

            _fachada.ModificarCliente(clienteBase, clienteModificado);

            await modal.FollowupAsync("✅ Cliente modificado correctamente.", ephemeral: true);
        }

    }
}
