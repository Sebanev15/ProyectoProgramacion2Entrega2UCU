using System.Collections.Generic;
using Library;
using Library.interfaces;
using Ucu.Poo.DiscordBot.Domain;

namespace Ucu.Poo.DiscordBot.Commands
{
    public static class Selecciones
    {
        public static Dictionary<ulong, List<Venta>> OpcionesVenta = new Dictionary<ulong, List<Venta>>();
        public static Dictionary<ulong, Venta> VentaSeleccionada = new Dictionary<ulong, Venta>();
        
        public static Dictionary<ulong, List<Cotizacion>> OpcionesCotizacion = new Dictionary<ulong, List<Cotizacion>>();
        public static Dictionary<ulong, Cotizacion> CotizacionSeleccionada = new Dictionary<ulong, Cotizacion>();
        
        public static Dictionary<ulong, List<Cliente>> OpcionesClientes = new Dictionary<ulong, List<Cliente>>();
        public static Dictionary<ulong, Cliente> ClienteSeleccionado = new Dictionary<ulong, Cliente>();
        
        public static Dictionary<ulong, List<Usuario>> OpcionesUsuarios = new Dictionary<ulong, List<Usuario>>();
        public static Dictionary<ulong, Usuario> UsuarioSeleccionado = new Dictionary<ulong,Usuario>();
        
        public static Dictionary<ulong, List<Administrador>> OpcionesAdmins = new Dictionary<ulong, List<Administrador>>();
        public static Dictionary<ulong, Administrador> AdminSeleccionado = new Dictionary<ulong,Administrador>();

        public static Dictionary<ulong, List<Usuario>> OpcionesVendedorInicial = new();
        public static Dictionary<ulong, Usuario> VendedorInicialSeleccionado = new();

        public static Dictionary<ulong, List<Usuario>> OpcionesVendedorAsignado = new();
        public static Dictionary<ulong, Usuario> VendedorAsignadoSeleccionado = new();
    }
}