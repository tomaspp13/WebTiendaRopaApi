using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaRopa.Modelos;
using WebTiendaRopa.DTOs;

namespace WebTiendaRopa.Repository
{
    public interface IUsuarioRepository<TEntity>
    {
        Task CrearUsuario(TEntity usuarioEnviado);
        Task GuardarUsuario();
        Task<TEntity>? ObtenerUsuarioPorMail(string mail);
        Task<TEntity>? ObtenerUsuarioPorId(int id);
        IEnumerable<TEntity> ValidarUsuario(Func<TEntity,bool>filtro);

    }
}
