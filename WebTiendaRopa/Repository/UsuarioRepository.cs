using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using TiendaRopa.Data.Context;
using TiendaRopa.Modelos;
using WebTiendaRopa.DTOs;

namespace WebTiendaRopa.Repository
{
    public class UsuarioRepository : IUsuarioRepository<Usuario>
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CrearUsuario(Usuario usuarioEnviado)
        {
           await _context.Usuarios.AddAsync(usuarioEnviado);
        }
        public async Task GuardarUsuario()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Usuario>? ObtenerUsuarioPorMail(string mail)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == mail);

            if (usuario == null)
            {
                return null;
            }

            return usuario;
        }
        public async Task<Usuario>? ObtenerUsuarioPorId(int id)
        {
            var respuesta = await _context.Usuarios.FindAsync(id);

            return respuesta;

        }
        public IEnumerable<Usuario> ValidarUsuario(Func<Usuario, bool> filtro)
        {
            return _context.Usuarios.Where(filtro).ToList();
        }
    }
}
