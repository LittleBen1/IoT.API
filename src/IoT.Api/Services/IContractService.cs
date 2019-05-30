using IoT.Api.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace IoT.Api.Services
{
    public interface IContractService
    {
        Task<IEnumerable<Contract>> ListAsync();
        Task<ContractResponse> SaveAsync(Contract contract);
        Task<ContractResponse> UpdateAsync(int id, Contract contract);
        Task<ContractResponse> DeleteAsync(int id);
        Task<IEnumerable<Contract>> ListAsyncPaged(int pageSize, int pageNumber);
    }
}
