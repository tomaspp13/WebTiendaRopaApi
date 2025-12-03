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
        Task<D>? ObtenerRopasPorId(int id);
        Task<List<L>>? FiltroRopa(FiltroRopaDto filtroRopa);
        Task<C>? AgregarRopa(RopaIngresoDto ropaNueva);
        bool ValidarIdRopa(int idRopa);
        bool ValidarIngresoRopa(RopaIngresoDto ropaIngreso);
        bool ValidarIdRopas(List<int> idRopas);   

    }
}
