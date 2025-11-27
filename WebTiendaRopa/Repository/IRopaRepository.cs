using Microsoft.AspNetCore.Mvc;
using TiendaRopa.Modelos;
using WebTiendaRopa.DTOs;

namespace WebTiendaRopa.Repository
{
    public interface IRopaRepository<TEntity>
    {
        IEnumerable<TEntity>? ObtenerRopas();
        Task<ActionResult<TEntity>?> ObtenerRopasPorId(int id);
        Task Add(Ropa ropa);
        Task Guardar();
        IEnumerable<TEntity> ValidarRopa(Func <TEntity,bool> filtro);

    }
}
