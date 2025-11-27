using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaRopa.Data.Context;
using TiendaRopa.Modelos;
using WebTiendaRopa.DTOs;

namespace WebTiendaRopa.Repository
{
    public class FacturaRepository : IFacturaRepository<Factura>
    {
        private readonly AppDbContext _context;
        public FacturaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CrearFactura(Factura facturaNueva)
        {
            _context.Factura.Add(facturaNueva);
            await _context.SaveChangesAsync();
        }

        public async Task<ActionResult<List<Factura>>> MostrarFacturaConDatosDelUsuarioYCompras(int idUsuario)
        {
            var facturas = await _context.Factura.Include(f => f.Usuario).Include(f => f.Compras).ThenInclude(fc => fc.Ropa).Where( f => f.IdUsuario == idUsuario).ToListAsync();

            return new ActionResult<List<Factura>>(facturas);
        }

        public async Task GuardarFactura()
        {
            await _context.SaveChangesAsync();
        }

        IEnumerable<Factura> IFacturaRepository<Factura>.ValidarFactura(Func<Factura, bool> filtro)
        {
            return _context.Factura.Where(filtro).ToList();
        }
    }
}
