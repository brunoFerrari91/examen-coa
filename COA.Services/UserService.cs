using COA.Data;
using COA.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace COA.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _userRepository.GetUsuarios();
        }

        public Usuario GetById(int id)
        {
            if (id > 0)
            {
                return _userRepository.GetUsuario(id);
            }
            else
            {
                throw new ArgumentOutOfRangeException("El Id debe ser mayor a cero");
            }
        }

        public void Create(Usuario usuario)
        {
            _userRepository.CreateUsuario(usuario);
        }

        public void Update(Usuario userToUpdate, Usuario usuario)
        {
            userToUpdate.UserName = usuario.UserName;
            userToUpdate.Nombre = usuario.Nombre;
            userToUpdate.Email = usuario.Email;
            userToUpdate.Telefono = usuario.Telefono;
            _userRepository.UpdateUsuario(userToUpdate);
        }

        public void Delete(Usuario usuario)
        {
            _userRepository.DeleteUsuario(usuario);
        }
    }
}
