using BCrypt.Net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using TiendaRopa.Data.Context;
using TiendaRopa.Modelos;
using WebTiendaRopa.DTOs;
using WebTiendaRopa.Repository;

namespace WebTiendaRopa.Servicios
{
    public class UsuarioServicios : IUsuarioServicios<UsuarioRespuestaTotalDto, UsuarioRespuestaDto>
    {

        private readonly IUsuarioRepository<Usuario> _usuarioRepository;
        public List<string> Errors { get; }
        public UsuarioServicios(IUsuarioRepository<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            Errors = new List<string>();
        }
        public async Task<ActionResult<UsuarioRespuestaTotalDto>?> CrearUsuario(UsuarioRegistroDto usuarioEnviado)
        {
            if(_usuarioRepository.ValidarUsuario(u => u.Email == usuarioEnviado.Email) != null) 
            {
                Errors.Add("Mail ya ingresado.Ingrese otro");
                return null;            
            }

            string hash = BCrypt.Net.BCrypt.HashPassword(usuarioEnviado.Contraseña);

            var usuario = new Usuario()
            {
                Nombre = usuarioEnviado.Nombre,
                Email = usuarioEnviado.Email,
                Contraseña = hash
            };

            await _usuarioRepository.CrearUsuario(usuario);
            await _usuarioRepository.GuardarUsuario();

            var usuarioDevuelto = new UsuarioRespuestaTotalDto()
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Contraseña = usuario.Contraseña,
                Email = usuario.Email
            };

            return usuarioDevuelto;

        }
        public async Task<ActionResult<UsuarioRespuestaDto>?> ObtenerUsuario(UsuarioIngresoDto usuarioEnviado)
        {
            if(_usuarioRepository.ValidarUsuario(u => u.Email == usuarioEnviado.Email) == null)
            {
                Errors.Add("Correo no encontrado");
                return null;
            }

            var resultado = await _usuarioRepository.ObtenerUsuario(usuarioEnviado);

            var datos = resultado.Value;

            bool esValida = BCrypt.Net.BCrypt.Verify(usuarioEnviado.Contraseña, datos.Contraseña);

            if (!esValida)
            {
                Errors.Add("Contraseña incorrecta");
                return null;
            }

            var usuario = new UsuarioRespuestaDto()
            {
                Nombre = datos.Nombre,
                Email = datos.Email,
            };

            return usuario;
        }
        public async Task<ActionResult<UsuarioRespuestaTotalDto>?> ObtenerUsuarioPorId(int id)
        {
            if(_usuarioRepository.ValidarUsuario(u => u.IdUsuario == id) == null) 
            {
                Errors.Add("Usuario no encontrado");
                return null;
            }

            var respuesta = await _usuarioRepository.ObtenerUsuarioPorId(id);
            var usuario = respuesta.Value;

            var usuarioEnviado = new UsuarioRespuestaTotalDto
            {
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Contraseña = usuario.Contraseña,
                IdUsuario = id
            };

            return usuarioEnviado;
        }
        public bool ValidarIdUsuario(int idUsuario)
        {
            if (_usuarioRepository.ValidarUsuario(u => u.IdUsuario == idUsuario) == null)
            {
                Errors.Add("Id isuario no encontrado");
                return false;
            }
            return true;
        }
        public bool ValidarIngresoMail(string correo)
        {
            if (_usuarioRepository.ValidarUsuario(u => u.Email == correo) != null)
            {
                Errors.Add("Email de usuario ya registrado");
                return false;
            }
            return true;
        }

        public bool ValidarIngresoContraseña(UsuarioIngresoDto ingresoUsuario)
        {


            //if (_usuarioRepository.ValidarUsuario(u => u.Email == ingresoUsuario.Email && u.Contraseña == ingreso.ci) != null)
            {
                Errors.Add("Email de usuario ya registrado");
                return false;
            }
            return true;
        }

    }
}
