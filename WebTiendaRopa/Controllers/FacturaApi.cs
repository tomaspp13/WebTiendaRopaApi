using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaRopa.Data.Context;
using TiendaRopa.Modelos;
using WebTiendaRopa.DTOs;
using WebTiendaRopa.Servicios;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace WebTiendaRopa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaApi : ControllerBase
    {

        private readonly IValidator<CompraDto> _validatorCompra;
        private readonly IUsuarioServicios<UsuarioRespuestaTotalDto, UsuarioRespuestaDto> _usuarioServicios;
        private readonly IFacturaServicios<FacturaRespuestaDto, FacturaRespuestaCompletaDto> _facturaServicios;

        public FacturaApi(IUsuarioServicios<UsuarioRespuestaTotalDto, UsuarioRespuestaDto> usuarioServicios, IFacturaServicios<FacturaRespuestaDto, FacturaRespuestaCompletaDto> facturaServicios, IValidator<CompraDto> validatorCompra)
        {
            _usuarioServicios = usuarioServicios;
            _validatorCompra = validatorCompra;
            _facturaServicios = facturaServicios;
        }

        [HttpPost("CrearFactura")]
        public async Task<ActionResult<FacturaRespuestaDto>> CrearFactura(CompraDto compraNueva)
        {

            var validacion = _validatorCompra.Validate(compraNueva);

            if (validacion != null)
            {
                return BadRequest(validacion.Errors);
            }

            var respuesta = await _facturaServicios.CrearFactura(compraNueva);

            var factura = respuesta.Value;

            return factura == null ? BadRequest(_facturaServicios.Errors) : CreatedAtAction(nameof(MostrarFacturaPorIdUsuario), new { idUsuario = factura.IdUsuario}, factura);
        }

        [HttpGet("MostrarFacturaPorIdUsuario/{idUsuario}")]
        public async Task<ActionResult<FacturaRespuestaDto>> MostrarFacturaPorIdUsuario(int idUsuario)
        {
            if(_usuarioServicios.ValidarIdUsuario(idUsuario) == false) { return BadRequest(_usuarioServicios.Errors); }

            var facturas = await _facturaServicios.MostrarFacturaConDatosDelUsuarioYCompras(idUsuario);

            return facturas == null || !facturas.Any() ? BadRequest(_facturaServicios.Errors) : Ok(facturas);
        }
    }
}
