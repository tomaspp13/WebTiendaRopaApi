using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using TiendaRopa.Modelos;
using WebTiendaRopa.DTOs;

namespace WebTiendaRopa.Repository
{
    public interface IRopaRepository<TEntity>
    {
        IEnumerable<TEntity>? ObtenerRopas();
        Task<TEntity>? ObtenerRopasPorId(int id);
        Task Add(Ropa ropa);
        Task Guardar();
        IEnumerable<TEntity> ValidarRopa(Expression<Func<Ropa, bool>> filtro);

    }
}
