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
        private readonly IRopasServicios<RopaRespuestaListadoDto, RopaRespuestaDetalles, RopaRespuestaCompleta> _ropasServicios;
        public FacturaApi(IUsuarioServicios<UsuarioRespuestaTotalDto, UsuarioRespuestaDto> usuarioServicios, IFacturaServicios<FacturaRespuestaDto, FacturaRespuestaCompletaDto> facturaServicios, IValidator<CompraDto> validatorCompra, IRopasServicios<RopaRespuestaListadoDto, RopaRespuestaDetalles, RopaRespuestaCompleta> ropasServicios)
        {
            _usuarioServicios = usuarioServicios;
            _validatorCompra = validatorCompra;
            _facturaServicios = facturaServicios;
            _ropasServicios = ropasServicios;
        }

        [HttpPost("CrearFactura")]
        public async Task<ActionResult<FacturaRespuestaDto>> CrearFactura(CompraDto compraNueva)
        {

            var validacion = _validatorCompra.Validate(compraNueva);

            if (!validacion.IsValid)
            {
                return BadRequest(validacion.Errors);
            }

            if(_usuarioServicios.ValidarIdUsuario(compraNueva.IdUsuario) == false)
            {   
                return BadRequest(_usuarioServicios.Errors);
            }

            if(_ropasServicios.ValidarIdRopas(compraNueva.IdCompras) == false) 
            { 
                return BadRequest(_ropasServicios.Errors); 
            }

            var factura = await _facturaServicios.CrearFactura(compraNueva);

            return CreatedAtAction(nameof(MostrarFacturaPorIdUsuario), new { idUsuario = factura.IdUsuario}, factura);
        }

        [HttpGet("MostrarFacturaPorIdUsuario/{idUsuario}")]
        public async Task<ActionResult<FacturaRespuestaDto>> MostrarFacturaPorIdUsuario(int idUsuario)
        {
            if (!_usuarioServicios.ValidarIdUsuario(idUsuario))
                return BadRequest(_usuarioServicios.Errors);

            var facturas = await _facturaServicios.MostrarFacturaConDatosDelUsuarioYCompras(idUsuario);

            if (facturas == null || !facturas.Any())
            {
                return BadRequest(_facturaServicios.Errors);
            }

            return Ok(facturas);
        }
    }
}
