using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using IoT.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using IoT.Api.Models;

namespace WebAPI.Controllers
{
    [Route("api/v1/contracts")]
    [ApiController]
    public class ContractsController : Controller
    {
        //private readonly AppSettings _appsettings;
        private readonly IContractService _service;
        private readonly IMapper _mapper;

        public ContractsController(IContractService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/v1/contracts
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([Microsoft.AspNetCore.Mvc.FromBody]Contract contractParam)
        {
            //var contract = _contractService.Authenticate(contractParam.Contractname, contractParam.Password);
            var contract = new Contract();
            if (contract == null)
                return BadRequest(new { message = "Contractname or password is incorrect" });

            return Ok(contract);
        }

        // POST: api/v1/contracts
        [HttpPost]
        public async Task<ActionResult<Contract>> PostAsync([FromBody] SaveContractResource resource)
        {
            if (!ModelState.IsValid)
                return null;
            BadRequest(ModelState);
            var contract = _mapper.Map<SaveContractResource, Contract>(resource);
            var result = await _service.SaveAsync(contract);

            if (!result.Success)
                return BadRequest(result.Message);

            var contractResource = _mapper.Map<Contract, ContractResource>(result.Contract);
            return Ok(contractResource);
        }


        public async Task<IEnumerable<ContractResource>> ListAsync()
        {
            var contracts = await _service.ListAsync();
            var resources = _mapper.Map<IEnumerable<Contract>, IEnumerable<ContractResource>>(contracts);

            return resources;
        }

        [HttpGet]
        public async Task<IEnumerable<ContractResource>> GetAsync([FromQuery(Name = "pageSize")] int pageSize = -1, [FromQuery(Name = "pageNumber")] int pageNumber = -1)
        {
            // Check if the source is null
            if (pageSize == -1 || pageNumber == -1)
                return await ListAsync();
            var contracts = await _service.ListAsyncPaged(pageSize, pageNumber);
            var resources = _mapper.Map<IEnumerable<Contract>, IEnumerable<ContractResource>>(contracts);
            return resources;
        }

        // GET: api/v1/contracts/id
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var contract = new Contract();
            //_service.Get(id);
            if (contract != null)
                return Ok(contract);
            return NotFound();
        }

        // PUT: api/v1/contracts/id
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] SaveContractResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contract = _mapper.Map<SaveContractResource, Contract>(resource);
            var result = await _service.UpdateAsync(id, contract);

            if (!result.Success)
                return BadRequest(result.Message);

            var contractResource = _mapper.Map<Contract, ContractResource>(result.Contract);

            return Ok(contractResource);
        }

        // DELETE: api/v1/contracts/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var contractResource = _mapper.Map<Contract, ContractResource>(result.Contract);
            return Ok(contractResource);
        }
    }
}
