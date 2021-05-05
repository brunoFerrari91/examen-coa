using COA.Data;
using COA.Data.Models;
using System;
using System.Collections.Generic;

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
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(null, "El ID debe ser mayor a cero");
            }
            var user = _userRepository.GetUsuario(id);
            if (user == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }
            return user;

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
