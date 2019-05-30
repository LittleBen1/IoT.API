using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IoT.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using IoT.Api.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/v1/homes")]
    [ApiController]
    public class HomesController : Controller
    {
        //private readonly AppSettings _appsettings;
        private readonly IHomeService _service;
        private readonly IMapper _mapper;

        public HomesController(IHomeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // POST: api/v1/homes
        [HttpPost]
        public async Task<ActionResult<Home>> PostAsync([FromBody] SaveHomeResource resource)
        {
            if (!ModelState.IsValid)
                return null;
            BadRequest(ModelState);
            var home = _mapper.Map<SaveHomeResource, Home>(resource);
            var result = await _service.SaveAsync(home);

            if (!result.Success)
                return BadRequest(result.Message);

            var homeResource = _mapper.Map<Home, HomeResource>(result.Home);
            return Ok(homeResource);
        }


        public async Task<IEnumerable<HomeResource>> ListAsync()
        {
            var homes = await _service.ListAsync();
            var resources = _mapper.Map<IEnumerable<Home>, IEnumerable<HomeResource>>(homes);

            return resources;
        }

        [HttpGet]
        public async Task<IEnumerable<HomeResource>> GetAsync([FromQuery(Name = "pageSize")] int pageSize = -1, [FromQuery(Name = "pageNumber")] int pageNumber = -1)
        {
            // Check if the source is null
            if (pageSize == -1 || pageNumber == -1)
                return await ListAsync();
            var homes = await _service.ListAsyncPaged(pageSize, pageNumber);
            var resources = _mapper.Map<IEnumerable<Home>, IEnumerable<HomeResource>>(homes);
            return resources;
        }

        // GET: api/v1/homes/id
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var home = new Home();
            //_service.Get(id);
            if (home != null)
                return Ok(home);
            return NotFound();
        }

        // PUT: api/v1/homes/id
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] SaveHomeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var home = _mapper.Map<SaveHomeResource, Home>(resource);
            var result = await _service.UpdateAsync(id, home);

            if (!result.Success)
                return BadRequest(result.Message);

            var homeResource = _mapper.Map<Home, HomeResource>(result.Home);

            return Ok(homeResource);
        }

        // DELETE: api/v1/homes/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var homeResource = _mapper.Map<Home, HomeResource>(result.Home);
            return Ok(homeResource);
        }
    }
}
