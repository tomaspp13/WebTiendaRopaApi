using Microsoft.AspNetCore.Mvc;
using WebTiendaRopa.DTOs;

namespace WebTiendaRopa.Servicios
{
    public interface IFacturaServicios<FacturaRespuestaDto, FacturaRespuestaCompletaDto>
    {
        public List<string> Errors { get; }
        Task<ActionResult<FacturaRespuestaCompletaDto>?> CrearFactura(CompraDto compraNueva);
        Task<IEnumerable<FacturaRespuestaDto>?> MostrarFacturaConDatosDelUsuarioYCompras(int idUsuario);
        bool ValidarIdFactura(int idUsuario);

    }
}
