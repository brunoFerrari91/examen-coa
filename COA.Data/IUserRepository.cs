using COA.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace COA.Data
{
    public interface IUserRepository
    {
        IEnumerable<Usuario> GetUsuarios();
        Usuario GetUsuario(int id);
        void CreateUsuario(Usuario usuario);
        void UpdateUsuario(Usuario usuario);
        void DeleteUsuario(Usuario usuario);
    }
}
