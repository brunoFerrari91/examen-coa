using COA.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace COA.Services
{
    public interface IUserService
    {
        IEnumerable<Usuario> GetAll();
        Usuario GetById(int id);
        void Create(Usuario usuario);
        void Update(Usuario usuario);
        void Delete(Usuario usuario);
    }
}
