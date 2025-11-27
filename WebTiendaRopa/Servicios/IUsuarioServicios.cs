using Microsoft.AspNetCore.Mvc;
using WebTiendaRopa.DTOs;

namespace WebTiendaRopa.Servicios
{
    public interface IUsuarioServicios<UsuarioRespuestaTotalDto, UsuarioRespuestaDto>
    {
        public List<string> Errors { get; }
        Task<ActionResult<UsuarioRespuestaTotalDto>?> CrearUsuario(UsuarioRegistroDto usuarioEnviado);
        Task<ActionResult<UsuarioRespuestaDto>?> ObtenerUsuario(UsuarioIngresoDto usuarioEnviado);
        Task<ActionResult<UsuarioRespuestaTotalDto>?> ObtenerUsuarioPorId(int id);
        bool ValidarIdUsuario(int idUsuario);
        bool ValidarIngresoMail(string correo);
    }
}