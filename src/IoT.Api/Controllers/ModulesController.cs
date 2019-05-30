using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IoT.Api.Repositories;
using IoT.Api.Communication;
using IoT.Api.Models;
using AutoMapper;
using IoT.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/v1/modules")]
    [ApiController]
    public class ModulesController : Controller
    {
        //private readonly AppSettings _appsettings;
        private readonly IModuleService _service;
        private readonly IMapper _mapper;

        public ModulesController(IModuleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/v1/modules
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([Microsoft.AspNetCore.Mvc.FromBody]Module moduleParam)
        {
            //var module = _moduleService.Authenticate(moduleParam.Modulename, moduleParam.Password);
            var module = new Module();
            if (module == null)
                return BadRequest(new { message = "Modulename or password is incorrect" });

            return Ok(module);
        }

        // POST: api/v1/modules
        [HttpPost]
        public async Task<ActionResult<Module>> PostAsync([FromBody] SaveModuleResource resource)
        {
            if (!ModelState.IsValid)
                return null;
            BadRequest(ModelState);
            var module = _mapper.Map<SaveModuleResource, Module>(resource);
            var result = await _service.SaveAsync(module);

            if (!result.Success)
                return BadRequest(result.Message);

            var moduleResource = _mapper.Map<Module, ModuleResource>(result.Module);
            return Ok(moduleResource);
        }


        public async Task<IEnumerable<ModuleResource>> ListAsync()
        {
            var modules = await _service.ListAsync();
            var resources = _mapper.Map<IEnumerable<Module>, IEnumerable<ModuleResource>>(modules);

            return resources;
        }

        [HttpGet]
        public async Task<IEnumerable<ModuleResource>> GetAsync([FromQuery(Name = "pageSize")] int pageSize = -1, [FromQuery(Name = "pageNumber")] int pageNumber = -1)
        {
            // Check if the source is null
            if (pageSize == -1 || pageNumber == -1)
                return await ListAsync();
            var modules = await _service.ListAsyncPaged(pageSize, pageNumber);
            var resources = _mapper.Map<IEnumerable<Module>, IEnumerable<ModuleResource>>(modules);
            return resources;
        }

        // GET: api/v1/modules/id
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var module = new Module();
            //_service.Get(id);
            if (module != null)
                return Ok(module);
            return NotFound();
        }

        // PUT: api/v1/modules/id
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] SaveModuleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var module = _mapper.Map<SaveModuleResource, Module>(resource);
            var result = await _service.UpdateAsync(id, module);

            if (!result.Success)
                return BadRequest(result.Message);

            var moduleResource = _mapper.Map<Module, ModuleResource>(result.Module);

            return Ok(moduleResource);
        }

        // DELETE: api/v1/modules/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var moduleResource = _mapper.Map<Module, ModuleResource>(result.Module);
            return Ok(moduleResource);
        }
    }
}
