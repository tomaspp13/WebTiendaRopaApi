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
        public async Task<UsuarioRespuestaTotalDto>? CrearUsuario(UsuarioRegistroDto usuarioEnviado)
        {
 
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
        public async Task<UsuarioRespuestaDto>? ObtenerUsuario(UsuarioIngresoDto usuarioEnviado)
        {
            var user = await _usuarioRepository.ObtenerUsuarioPorMail(usuarioEnviado.Email);

            var usuario = new UsuarioRespuestaDto()
            {
                Nombre = user.Nombre,
                Email = user.Email,
            };

            return usuario;
        }
        public async Task<UsuarioRespuestaTotalDto>? ObtenerUsuarioPorId(int id)
        {
            Errors.Clear();

            if (_usuarioRepository.ValidarUsuario(u => u.IdUsuario == id) == null) 
            {
                Errors.Add("Usuario no encontrado");
                return null;
            }

            var respuesta = await _usuarioRepository.ObtenerUsuarioPorId(id);

            var usuarioEnviado = new UsuarioRespuestaTotalDto
            {
                Nombre = respuesta.Nombre,
                Email = respuesta.Email,
                Contraseña = respuesta.Contraseña,
                IdUsuario = id
            };

            return usuarioEnviado;
        }
        public bool ValidarIdUsuario(int idUsuario)
        {

            Errors.Clear();
            var resultado = _usuarioRepository.ValidarUsuario(u => u.IdUsuario == idUsuario);

            if (resultado == null || !resultado.Any())
            {
                Errors.Add("Id isuario no encontrado");
                return false;
            }
            return true;
        }
        public async Task<bool> ValidarRegistroUsuario(UsuarioRegistroDto ingresoUsuario)
        {
            Errors.Clear();
            var usuario = await _usuarioRepository.ObtenerUsuarioPorMail(ingresoUsuario.Email);

            if (usuario != null)
            {
                Errors.Add("Mail ya ingresado.Ingrese otro");
                return false;
            }

            return true;
        }
        public async Task<bool> ValidarIngresoUsuario(UsuarioIngresoDto ingresoUsuario)
        {

            Errors.Clear();
            var usuario = await _usuarioRepository.ObtenerUsuarioPorMail(ingresoUsuario.Email);

            if (usuario == null)
            {
                Errors.Add("Mail no encontrado.Ingrese otro");
                return false;
            }

            bool esValida = BCrypt.Net.BCrypt.Verify(ingresoUsuario.Contraseña, usuario.Contraseña);

            if (!esValida)
            {
                Errors.Add("Contraseña incorrecta");
                return false;
            }

            return true;
        }

    }
}
