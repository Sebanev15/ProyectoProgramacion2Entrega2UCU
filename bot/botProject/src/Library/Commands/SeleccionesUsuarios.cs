using System.Collections.Generic;
using Library;
using Library.interfaces;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands
{
    public static class SeleccionesUsuarios
    {
        public static Dictionary<ulong, List<Venta>> OpcionesVenta = new Dictionary<ulong, List<Venta>>();
        public static Dictionary<ulong, List<Cotizacion>> OpcionesCotizacion = new Dictionary<ulong, List<Cotizacion>>();
        public static Dictionary<ulong, List<Cliente>> OpcionesClientes = new Dictionary<ulong, List<Cliente>>();
        public static Dictionary<ulong, List<Usuario>> OpcionesUsuarios = new Dictionary<ulong, List<Usuario>>();
    }
}