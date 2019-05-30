using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using IoT.Api.Services;
using AutoMapper;
using IoT.Api.Resources;
using IoT.Api.Models;
using Microsoft.AspNetCore.Mvc;
using IoT.Api.Models.ETypes;

namespace IoT.Api.Controllers
{

    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UsersController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/v1/users
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCredentialsResource userCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<UserCredentialsResource, User>(userCredentials);

            var response = await _service.CreateUserAsync(user, ERole.Common);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var userResource = _mapper.Map<User, UserResource>(response.User);
            return Ok(userResource);
        }

        [AllowAnonymous]
        public async Task<IEnumerable<UserResource>> ListAsync()
        {
            var users = await _service.ListAsync();
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);

            return resources;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<UserResource>> GetAsync([FromQuery(Name = "pageSize")] int pageSize = -1, [FromQuery(Name = "pageNumber")] int pageNumber = -1)
        {
            // Check if the source is null
            if (pageSize == -1 || pageNumber == -1)
                return await ListAsync();
            var users = await _service.ListAsyncPaged(pageSize,pageNumber);
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            return resources;
        }

        // GET: api/v1/users/id
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var user = new User();
                //_service.Get(id);
            if (user != null)
                return Ok(user);
            return NotFound();
        }

        // PUT: api/v1/users/id
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<SaveUserResource, User>(resource);
            var result = await _service.UpdateAsync(id, user);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.User);

            return Ok(userResource);
        }

        // DELETE: api/v1/users/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }
    }
}
