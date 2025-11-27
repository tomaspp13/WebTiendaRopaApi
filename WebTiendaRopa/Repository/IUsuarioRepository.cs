using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaRopa.Modelos;
using WebTiendaRopa.DTOs;

namespace WebTiendaRopa.Repository
{
    public interface IUsuarioRepository<TEntity>
    {
        Task CrearUsuario(Usuario usuarioEnviado);
        Task GuardarUsuario();
        Task<ActionResult<Usuario>?> ObtenerUsuario(UsuarioIngresoDto usuarioEnviado);
        Task<ActionResult<Usuario>?> ObtenerUsuarioPorId(int id);
        IEnumerable<TEntity> ValidarUsuario(Func<TEntity,bool>filtro);

    }
}
