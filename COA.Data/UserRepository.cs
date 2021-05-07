using COA.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace COA.Data
{
    public class UserRepository : IUserRepository
    {
        protected readonly ExamenDBContext _context;
        public UserRepository(ExamenDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            return _context.Usuarios.OrderByDescending(u => u.IdUsuario).ToList();
        }

        public IEnumerable<Usuario> GetUsuarios(int page)
        {
            int skipSize = (page - 1) * 10;
            //negative skip size should not return any page (negative or high numbers)
            if (skipSize < 0)
            {
                return new List<Usuario>();
            }
            return _context.Usuarios
                .OrderByDescending(u => u.IdUsuario)
                .Skip(skipSize)
                .Take(10)
                .ToList();
        } 

        public Usuario GetUsuario(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public void CreateUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void UpdateUsuario(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteUsuario(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
    }
}
