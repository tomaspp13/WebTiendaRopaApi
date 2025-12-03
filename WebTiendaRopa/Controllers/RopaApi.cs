using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TiendaRopa.Data.Context;
using TiendaRopa.Modelos;
using WebTiendaRopa.DTOs;
using WebTiendaRopa.Servicios;

namespace WebTiendaRopa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RopaApi : ControllerBase
    {

        private readonly IRopasServicios<RopaRespuestaListadoDto,RopaRespuestaDetalles,RopaRespuestaCompleta> _ropasServicios;

        private readonly IValidator<FiltroRopaDto> _validationsFiltroRopa;
        private readonly IValidator<RopaIngresoDto> _validationsIngresoRopa;        
        public RopaApi( 
              IRopasServicios<RopaRespuestaListadoDto, RopaRespuestaDetalles, RopaRespuestaCompleta> ropasServicios,
              IValidator<FiltroRopaDto> validationsFiltroRopa,
              IValidator<RopaIngresoDto> validationsIngresoRopa
            )
        {
            _ropasServicios = ropasServicios;
            _validationsFiltroRopa = validationsFiltroRopa;
            _validationsIngresoRopa = validationsIngresoRopa;
        }

        [HttpGet("ObtenerRopas")]
        public IEnumerable<RopaRespuestaListadoDto>? ObtenerRopas()
        {
            return  _ropasServicios.ObtenerRopas();            
        }

        [HttpGet("ObtenerRopasPorId/{id}")]
        public async Task<ActionResult<RopaRespuestaDetalles>> ObtenerRopasPorId(int id)
        {
            if(_ropasServicios.ValidarIdRopa(id) == false) { return BadRequest(_ropasServicios.Errors); }

            var resultado = await _ropasServicios.ObtenerRopasPorId(id);

            return Ok(resultado);        
        }

        [HttpPost("FiltroRopa")]
        public async Task<ActionResult<List<FiltroRopaDto>>> FiltroRopa(FiltroRopaDto filtroRopa)
        {

            var validacion = await _validationsFiltroRopa.ValidateAsync(filtroRopa);
            if (validacion != null) { return BadRequest(validacion.Errors); }

            var ropaFiltradas = await _ropasServicios.FiltroRopa(filtroRopa);

            return Ok(ropaFiltradas);

        }

        [HttpPost("AgregarRopa")]
        public async Task<ActionResult<RopaRespuestaCompleta>?> AgregarRopa(RopaIngresoDto ropaNueva)
        {

            var validar = _validationsIngresoRopa.Validate(ropaNueva);
            if (validar != null) { return BadRequest(validar.Errors); }

            if (_ropasServicios.ValidarIngresoRopa(ropaNueva) == false) { return BadRequest(_ropasServicios.Errors); }

            var ropa = await _ropasServicios.AgregarRopa(ropaNueva);

            return CreatedAtAction(nameof(ObtenerRopasPorId), new { id = ropa.Id }, ropa);

        }

    }
}
