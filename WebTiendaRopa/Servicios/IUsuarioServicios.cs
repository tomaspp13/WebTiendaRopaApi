using Microsoft.AspNetCore.Mvc;
using WebTiendaRopa.DTOs;

namespace WebTiendaRopa.Servicios
{
    public interface IUsuarioServicios<UsuarioRespuestaTotalDto, UsuarioRespuestaDto>
    {
        public List<string> Errors { get; }
        Task<UsuarioRespuestaTotalDto>? CrearUsuario(UsuarioRegistroDto usuarioEnviado);
        Task<UsuarioRespuestaDto>? ObtenerUsuario(UsuarioIngresoDto usuarioEnviado);
        Task<UsuarioRespuestaTotalDto>? ObtenerUsuarioPorId(int id);
        bool ValidarIdUsuario(int idUsuario);
        Task<bool> ValidarIngresoUsuario(UsuarioIngresoDto ingresoUsuario);
        Task<bool> ValidarRegistroUsuario(UsuarioRegistroDto ingresoUsuario);
    }
}