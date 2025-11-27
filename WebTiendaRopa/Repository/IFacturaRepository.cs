using Microsoft.AspNetCore.Mvc;
using TiendaRopa.Modelos;
using WebTiendaRopa.DTOs;

namespace WebTiendaRopa.Repository
{
    public interface IFacturaRepository <TEntity>
    {
        Task CrearFactura(Factura facturaNueva);
        Task<ActionResult<List<Factura>>> MostrarFacturaConDatosDelUsuarioYCompras(int idUsuario);
        Task GuardarFactura();
        IEnumerable<TEntity> ValidarFactura(Func<TEntity, bool> filtro);

    }
}
