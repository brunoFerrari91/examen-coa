using AutoMapper;
using COA.Api.Resources;
using COA.Data.Models;
using COA.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace COA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        public UsersController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Mostrar información de todos los usuarios
        /// </summary>
        ///<response code="200">Pedido realizado correctamente</response>
        ///<response code="400">Error de validación</response>
        ///<response code="500">"Error interno del servidor"</response>
        [HttpGet]
        public IActionResult GetByPage([FromQuery]int page)
        {
            IEnumerable<Usuario> users;
            if (page == 0)
            {
                users = _service.GetAll();                
            }
            else
            {
                users = _service.GetPage(page);
            }

            var usersResource = _mapper.Map<IEnumerable<Usuario>, IEnumerable<UserResource>>(users);
            return Ok(usersResource);
        }

        /// <summary>
        /// Mostrar información de un usuario específico por su ID
        /// </summary>
        /// <param name="id">Id del usuario a mostrar</param>
        ///<response code="200">Pedido realizado correctamente</response>
        ///<response code="400">Error de validación</response>
        ///<response code="400">Id menor a cero</response>
        ///<response code="404">Id no encontrado</response>
        ///<response code="500">"Error interno del servidor"</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _service.GetById(id);
            var userResource = _mapper.Map<Usuario, UserResource>(user);
            return Ok(userResource);
        }

        /// <summary>
        /// Agregar un nuevo usuario a la base de datos
        /// </summary>
        /// <param name="user">Datos del usuario a agregar</param>
        ///<response code="200">Pedido realizado correctamente</response>
        ///<response code="400">Error de validación</response>
        ///<response code="500">"Error interno del servidor"</response>
        [HttpPost]
        public IActionResult Post([FromBody] UserResource user)
        {

            var userToCreate = _mapper.Map<UserResource, Usuario>(user);
            _service.Create(userToCreate);
            return Ok();
        }

        /// <summary>
        /// Actualizar los datos de un usuario ya existente
        /// </summary>
        /// <param name="id">Id del usuario a actualizar</param>
        /// <param name="user">Datos a actualizar o persistir</param>
        ///<response code="200">Pedido realizado correctamente</response>
        ///<response code="400">Error de validación</response>
        ///<response code="400">Id menor a cero</response>
        ///<response code="404">Id no encontrado</response>
        ///<response code="500">"Error interno del servidor"</response>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserResource user)
        {
            var userToUpdate = _service.GetById(id);
            var userResource = _mapper.Map<UserResource, Usuario>(user);
            _service.Update(userToUpdate, userResource);
            return Ok();
        }

        /// <summary>
        /// Eliminar un usuario específico de la base de datos
        /// </summary>
        /// <param name="id">ID del usuario a eliminar</param>
        ///<response code="200">Pedido realizado correctamente</response>
        ///<response code="400">Error de validación</response>
        ///<response code="400">Id menor a cero</response>
        ///<response code="404">Id no encontrado</response>
        ///<response code="500">"Error interno del servidor"</response>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userToDelete = _service.GetById(id);
            _service.Delete(userToDelete);
            return Ok();
        }
    }
}
