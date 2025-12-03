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

            if (await _usuarioServicio.ValidarIngresoUsuario(usuarioEnviado) == false)
            {
                return BadRequest(_usuarioServicio.Errors);
            }

            var usuario = _usuarioServicio.ObtenerUsuario(usuarioEnviado);

            return Ok(usuario);
        }

        [HttpPost("CrearUsuario")]
        public async Task<ActionResult<UsuarioIngresoDto>>? CrearUsuario(UsuarioRegistroDto usuarioEnviado)
        {
            var validar = _validatorUsuarioRegistro.Validate(usuarioEnviado);
            if(validar != null) { return BadRequest(validar.Errors); }

            if(await _usuarioServicio.ValidarRegistroUsuario(usuarioEnviado) == false) { return BadRequest(_usuarioServicio.Errors); }

            var usuario = await _usuarioServicio.CrearUsuario(usuarioEnviado);
  
            return CreatedAtAction(nameof(ObtenerUsuarioPorId), new { id = usuario.IdUsuario}, usuario);

        }

        [HttpGet("ObtenerUsuarioPorId")]
        public async Task<ActionResult<UsuarioRespuestaTotalDto>> ObtenerUsuarioPorId(int id)
        {
            if (_usuarioServicio.ValidarIdUsuario(id) == false) { return BadRequest         (_usuarioServicio.Errors); }

            var respuesta = await _usuarioServicio.ObtenerUsuarioPorId(id);
            return Ok(respuesta);

        }

    }
}
