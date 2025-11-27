using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using TiendaRopa.Data.Context;
using TiendaRopa.Modelos;
using WebTiendaRopa.DTOs;
using WebTiendaRopa.Repository;
using WebTiendaRopa.Servicios;

namespace WebTiendaRopa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioApi : ControllerBase
    {

        private readonly IUsuarioServicios<UsuarioRespuestaTotalDto,UsuarioRespuestaDto> _usuarioServicio;
        private IValidator<UsuarioRegistroDto> _validatorUsuarioRegistro;
        private IValidator<UsuarioIngresoDto> _validatorUsuarioIngreso;

        public UsuarioApi(IValidator<UsuarioRegistroDto> validatorUsuarioRegistro, IUsuarioServicios<UsuarioRespuestaTotalDto, UsuarioRespuestaDto> usuarioServicio, IValidator<UsuarioIngresoDto> validatorUsuarioIngreso)
        {
            _usuarioServicio = usuarioServicio;
            _validatorUsuarioRegistro = validatorUsuarioRegistro;
            _validatorUsuarioIngreso = validatorUsuarioIngreso;
        }

        [HttpPost("ObtenerUsuario")]
        public async Task<ActionResult<UsuarioRespuestaDto>> ObtenerUsuario(UsuarioIngresoDto usuarioEnviado)
        {
            var validacion = _validatorUsuarioIngreso.Validate(usuarioEnviado);
            if (validacion != null) { return BadRequest(validacion.Errors); }

            var resultado = await _usuarioServicio.ObtenerUsuario(usuarioEnviado);
            return resultado == null ? BadRequest(_usuarioServicio.Errors) : Ok(resultado);
        }

        [HttpPost("CrearUsuario")]
        public async Task<ActionResult<UsuarioIngresoDto>?> CrearUsuario(UsuarioRegistroDto usuarioEnviado)
        {
            var validar = _validatorUsuarioRegistro.Validate(usuarioEnviado);
            if(validar != null) { return BadRequest(validar.Errors); }

            //if(_usuarioServicio.ValidarIngresoUsuario(string correo) == false) { return BadRequest(_usuarioServicio.Errors); }

            var resultado = await _usuarioServicio.CrearUsuario(usuarioEnviado);
            var usuario = resultado.Value;
            return usuario == null? BadRequest(_usuarioServicio.Errors) : CreatedAtAction(nameof(ObtenerUsuarioPorId), new { id = usuario.IdUsuario}, usuario);

        }

        [HttpGet("ObtenerUsuarioPorId")]
        public async Task<ActionResult<UsuarioRespuestaTotalDto>> ObtenerUsuarioPorId(int id)
        {

            var respuesta = await _usuarioServicio.ObtenerUsuarioPorId(id);
            return respuesta == null ? BadRequest(_usuarioServicio.Errors) : Ok(respuesta.Value);

        }

    }
}
