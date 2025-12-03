using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaRopa.Data.Context;
using TiendaRopa.Modelos;
using WebTiendaRopa.DTOs;
using WebTiendaRopa.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebTiendaRopa.Servicios
{
    public class RopaServicios : IRopasServicios<RopaRespuestaListadoDto, RopaRespuestaDetalles, RopaRespuestaCompleta>
    {

        private readonly IRopaRepository<Ropa> _ropaRepository;
        public List<string> Errors { get; }
        public RopaServicios(IRopaRepository<Ropa> ropaRepository)
        {
            _ropaRepository = ropaRepository;
            Errors = new List<string>();
        }
        public async Task<RopaRespuestaCompleta>? AgregarRopa(RopaIngresoDto ropaNueva)
        {

            var ropa = new Ropa()
            {
                Name = ropaNueva.Name,
                Description = ropaNueva.Description,
                Tipo = ropaNueva.Tipo,
                Stock = ropaNueva.Stock,
                UrlRopa = ropaNueva.UrlRopa,
                Precio = ropaNueva.Precio,
                Tela = ropaNueva.Tela
            };

            await _ropaRepository.Add(ropa);
            await _ropaRepository.Guardar();

            var ropaDevuelta = new RopaRespuestaCompleta()
            {
                Id = ropa.Id,
                Name = ropa.Name,
                Description = ropa.Description,
                Tipo = ropa.Tipo,
                Stock = ropa.Stock,
                UrlRopa = ropa.UrlRopa,
                Precio = ropa.Precio,
                Tela = ropa.Tela
            };

            return ropaDevuelta;

        }
        public async Task<List<RopaRespuestaListadoDto>>? FiltroRopa(FiltroRopaDto filtroRopa)
        {

            var respuesta = _ropaRepository.ObtenerRopas();

            if (respuesta == null) { return null; }

            if (!string.IsNullOrWhiteSpace(filtroRopa.Nombre))
            {
                respuesta = respuesta.Where(r => r.Name != null && r.Name.Contains(filtroRopa.Nombre));
            }
            if (!string.IsNullOrWhiteSpace(filtroRopa.Tipo))
            {
                respuesta = respuesta.Where(r => r.Tipo != null && r.Tipo.Contains(filtroRopa.Tipo));
            }
            if (!string.IsNullOrWhiteSpace(filtroRopa.Tela))
            {
                respuesta = respuesta.Where(r => r.Tela != null && r.Tela.Contains(filtroRopa.Tela));
            }
            if (filtroRopa.PrecioMax > 0 && filtroRopa.PrecioMax > filtroRopa.PrecioMin)
            {
                respuesta = respuesta.Where(r => r.Precio <= filtroRopa.PrecioMax);
            }
            if (filtroRopa.PrecioMin >= 0 && filtroRopa.PrecioMin < filtroRopa.PrecioMax)
            {
                respuesta = respuesta.Where(r => r.Precio >= filtroRopa.PrecioMin);
            }

            var ropaFiltradas = respuesta.Select(r => new RopaRespuestaListadoDto
            {
                Name = r.Name,  
                Precio = r.Precio,
                UrlRopa = r.UrlRopa  
            }).ToList();

            return ropaFiltradas;
        }
        public IEnumerable<RopaRespuestaListadoDto>? ObtenerRopas()
        {
           var respuesta = _ropaRepository.ObtenerRopas();

            if(respuesta != null)
            {
                var ropa = respuesta.Select(r => new RopaRespuestaListadoDto
                {
                    Name = r.Name,    
                    Precio = r.Precio,
                    UrlRopa = r.UrlRopa

                }).ToList();

                return ropa;
            }

            return null;

        }
        public async Task<RopaRespuestaDetalles>? ObtenerRopasPorId(int id)
        {
            
            var ropa = await _ropaRepository.ObtenerRopasPorId(id);

            if (ropa == null) { Errors.Clear(); return null; }

            return new RopaRespuestaDetalles()
            {

                Name = ropa.Name,
                Descripcion = ropa.Description,
                Stock = ropa.Stock,
                Precio = ropa.Precio,
                Tela = ropa.Tela,
                UrlRopa = ropa.UrlRopa

            };

        }
        public bool ValidarIdRopa(int idRopa)
        {

            Errors.Clear();

            if (!_ropaRepository.ValidarRopa(r => r.Id == idRopa).Any())
            {
                Errors.Add("Id ropa no encontrado");
                return false;
            }
            return true;
        }
        public bool ValidarIngresoRopa(RopaIngresoDto ropaIngreso)
        {
            Errors.Clear();

            if (_ropaRepository.ValidarRopa(r => r.UrlRopa == ropaIngreso.UrlRopa && r.Name == ropaIngreso.Name) != null)
            {
                Errors.Add("Url y nombre de ropa ya ingresado");
                return false;
            }

            if (_ropaRepository.ValidarRopa(r => r.UrlRopa == ropaIngreso.UrlRopa) != null)
            {
                Errors.Add("Url de ropa ya ingresado");
                return false;
            }

            if (_ropaRepository.ValidarRopa(r => r.Name == ropaIngreso.Name) != null)
            {
                Errors.Add("Nombre de ropa ya ingresado");
                return false;
            }

            return true;

        }
        public bool ValidarIdRopas(List<int> idRopas)
        {

            Errors.Clear();

            var ropas = _ropaRepository.ValidarRopa(r => idRopas.Contains(r.Id));

            if(ropas == null || !ropas.Any())
            {
                Errors.Add("No se encontro ninguna ropa seleccionada");
                return false;
            }

            if(ropas.Count() != idRopas.Count) {
                Errors.Add("No coincide la cantidad de ropa seleccionada con la registrada");
                return false;
            }

            return true;
        }

    }
}

