using AutoMapper;
using COA.Api.Resources;
using COA.Data.Models;
using COA.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


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
            try
            {
                var user = _service.GetById(id);
                if (user == null)
                {   
                    return NotFound();
                }
                var userResource = _mapper.Map<Usuario, UserResource>(user);
                return Ok(userResource);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserResource user)
        {
            try
            {
                var userToCreate = _mapper.Map<UserResource, Usuario>(user);
                _service.Create(userToCreate);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserResource user)
        {
            try
            {
                var userToUpdate = _service.GetById(id);
                if (userToUpdate == null)
                {
                    return NotFound();
                }
                var userResource = _mapper.Map<UserResource, Usuario>(user);
                _service.Update(userToUpdate, userResource);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var userToDelete= _service.GetById(id);
                if (userToDelete == null)
                {
                    return NotFound();
                }
                _service.Delete(userToDelete);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
