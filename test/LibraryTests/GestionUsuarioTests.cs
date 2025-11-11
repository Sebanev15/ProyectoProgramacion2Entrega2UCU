using NUnit.Framework;
using Library.interfaces;
using System.Collections.Generic;
using System;
using System.IO;
using Library;

namespace LibraryTests    
{                    
    [TestFixture]
    public class GestionUsuarioTests
    {
        private Usuario usuario;
        private Administrador _administrador;
        private GestionCliente _gestionCliente;
        private GestionCliente _gestionCliente2;
        private GestionUsuario _gestionUsuario;
        private Vendedor _vendedor1;
        private Vendedor _vendedor2;
        private Cliente _cliente;

        [SetUp]
        public void SetUp()
        {            
            DateTime fechaN = new DateTime(2024, 10, 20);
            _gestionUsuario = new GestionUsuario();
            _gestionCliente = new GestionCliente();
            _gestionCliente2 = new GestionCliente();
            _administrador = new Administrador("admin","correoAdmin@gmail.com", "123456789", _gestionUsuario,_gestionCliente);
            usuario = new Usuario("juan","correo@gmail.com","099123456", _gestionUsuario, _gestionCliente);
            _vendedor1 = new Vendedor("andres", "correoVendedor1@gmail.com", "321654987", _gestionUsuario,_gestionCliente);
            _vendedor2 = new Vendedor("beatriz", "correoVendedor2@gmail.com", "321456987", _gestionUsuario,_gestionCliente2);
            _cliente = new Cliente("camilo", "Martinez", "132456987", "correoVendedor2@gmail.com","M",fechaN);
        }

        [Test]
        public void CrearUsuario()
        {            
            Assert.That(_gestionUsuario.Usuarios.Count, Is.EqualTo(0));
            _gestionUsuario.CrearUsuario(_administrador, usuario);
            Assert.That(_gestionUsuario.Usuarios.Count, Is.EqualTo(1));
        }
        
        [Test]
        public void SuspenderUsuario()
        {
            Assert.That(!usuario.EstaSuspendido);
            _gestionUsuario.SuspenderUsuario(_administrador, usuario);
            Assert.That(usuario.EstaSuspendido);
        }
        
        [Test]
        public void ReactivarUsuario()
        {
            _gestionUsuario.SuspenderUsuario(_administrador, usuario);
            Assert.That(usuario.EstaSuspendido);
            _gestionUsuario.ReactivarUsuario(_administrador, usuario);
            Assert.That(!usuario.EstaSuspendido);
        }
        [Test]
        public void EliminarUsuario()
        {            
            _gestionUsuario.CrearUsuario(_administrador, usuario);
            Assert.That(_gestionUsuario.Usuarios.Count, Is.EqualTo(1));
            _gestionUsuario.EliminarUsuario(_administrador, usuario);
            Assert.That(_gestionUsuario.Usuarios.Count, Is.EqualTo(0));
        }
        
        [Test]
        public void AsignarOtroVendedor()
        {        
            _vendedor1.GestionCliente.AgregarCliente(_cliente);
            _gestionUsuario.AsignarOtroVendedor(_vendedor1,_vendedor2,_cliente);
            Assert.That(_vendedor1.GestionCliente.Clientes.Contains(_cliente),Is.False);
            Assert.That(_vendedor2.GestionCliente.Clientes.Contains(_cliente),Is.True);
        }
    }
}