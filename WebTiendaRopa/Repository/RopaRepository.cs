using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaRopa.Data.Context;
using TiendaRopa.Modelos;
using WebTiendaRopa.DTOs;

namespace WebTiendaRopa.Repository
{
    public class RopaRepository : IRopaRepository<Ropa>
    {

        private readonly AppDbContext _context;
        public RopaRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Ropa>? ObtenerRopas()
        {
            return _context.Ropa.AsQueryable();
        }
        public async Task<ActionResult<Ropa>?> ObtenerRopasPorId(int id)
        {
            return await _context.Ropa.FindAsync(id);
       
        }
        public async Task Add(Ropa ropa)
        {
            await _context.AddAsync(ropa);
        }
        public async Task Guardar()
        {
            await _context.SaveChangesAsync();
        }
        IEnumerable<Ropa> IRopaRepository<Ropa>.ValidarRopa(Func<Ropa, bool> filtro)
        {
            return _context.Ropa.Where(filtro).ToList(); ;
        }
    }
}
