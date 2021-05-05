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

        [HttpGet]
        public IActionResult Get()
        {
            var users = _service.GetAll();
            var usersResource = _mapper.Map<IEnumerable<Usuario>, IEnumerable<UserResource>>(users);
            return Ok(usersResource);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _service.GetById(id);
            var userResource = _mapper.Map<Usuario, UserResource>(user);
            return Ok(userResource);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserResource user)
        {

            var userToCreate = _mapper.Map<UserResource, Usuario>(user);
            _service.Create(userToCreate);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserResource user)
        {
            var userToUpdate = _service.GetById(id);
            var userResource = _mapper.Map<UserResource, Usuario>(user);
            _service.Update(userToUpdate, userResource);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userToDelete = _service.GetById(id);
            _service.Delete(userToDelete);
            return Ok();
        }
    }
}
