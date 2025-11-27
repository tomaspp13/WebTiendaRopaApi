using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaRopa.Modelos;
using WebTiendaRopa.DTOs;

namespace WebTiendaRopa.Servicios
{
    public interface IRopasServicios<L,D,C>
    {
        public List<string> Errors { get; }
        IEnumerable<L>? ObtenerRopas();
        Task<ActionResult<D>?> ObtenerRopasPorId(int id);
        Task<ActionResult<List<L>>?> FiltroRopa(FiltroRopaDto filtroRopa);
        Task<ActionResult<C>?> AgregarRopa(RopaIngresoDto ropaNueva);
        bool ValidarIdRopa(int idRopa);
        bool ValidarIngresoRopa(FiltroRopaDto ropaIngreso);
        bool ValidarIngresoRopa(RopaIngresoDto ropaIngreso);

    }
}
