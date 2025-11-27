using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using TiendaRopa.Modelos;
using WebTiendaRopa.DTOs;
using WebTiendaRopa.Repository;

namespace WebTiendaRopa.Servicios
{
    public class FacturaServicios : IFacturaServicios<FacturaRespuestaDto, FacturaRespuestaCompletaDto>
    {
        private readonly IFacturaRepository<Factura> _facturaRepository;
        private readonly IUsuarioServicios<UsuarioRespuestaTotalDto, UsuarioRespuestaDto> _usuarioServicio;
        public List<string> Errors { get; }
        public FacturaServicios(IFacturaRepository<Factura> facturaRepository, IUsuarioServicios<UsuarioRespuestaTotalDto, UsuarioRespuestaDto> usuarioServicio)
        {
            _facturaRepository = facturaRepository;
            _usuarioServicio = usuarioServicio;
            Errors = new List<string>();
        }
        public async Task<ActionResult<FacturaRespuestaCompletaDto>?> CrearFactura(CompraDto compraNueva)
        {

            var factura = new Factura()
            {
                Precio = compraNueva.Precio,
                Fecha = compraNueva.Fecha,
                IdUsuario = compraNueva.IdUsuario,
                Compras = new List<FacturaCompras>()

            };

            await _facturaRepository.CrearFactura(factura);

            await _facturaRepository.GuardarFactura();

            foreach (var nc in compraNueva.IdCompras)
            {
                factura.Compras.Add(new FacturaCompras()
                {
                    IdFactura = factura.IdFactura,
                    IdRopa = nc

                });
            }

            await _facturaRepository.GuardarFactura();

            return new FacturaRespuestaCompletaDto()
            {
                Precio = factura.Precio,
                Fecha = factura.Fecha,
                IdUsuario = factura.IdUsuario,
                Compras = factura.Compras
            };

        }
        public async Task<IEnumerable<FacturaRespuestaDto>?> MostrarFacturaConDatosDelUsuarioYCompras(int idUsuario)
        {

            var datos = await _facturaRepository.MostrarFacturaConDatosDelUsuarioYCompras(idUsuario);

            var respuesta = datos.Value;

            return respuesta != null ? respuesta.Select(d => new FacturaRespuestaDto()
            {
                Email = d.Usuario.Email,
                Fecha = d.Fecha,
                Nombre = d.Usuario.Nombre,
                Precio = d.Precio,

                Compras = d.Compras.Select(c => new RopaRespuestaListadoDto
                {
                    Name = c.Ropa.Name,
                    UrlRopa = c.Ropa.UrlRopa

                }).ToList()

            }).ToList() : null;
        }

        public bool ValidarIdFactura(int idFactura)
        {
            if (_facturaRepository.ValidarFactura(f => f.IdFactura == idFactura) == null)
            {
                Errors.Add("Id factura no encontrado");
                return false;
            }
            return true;
        }


    }

}
