using IoT.Api.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace IoT.Api.Services
{
    public interface IModuleService
    {
        Task<IEnumerable<Module>> ListAsync();
        Task<ModuleResponse> SaveAsync(Module module);
        Task<ModuleResponse> UpdateAsync(int id, Module module);
        Task<ModuleResponse> DeleteAsync(int id);
        Task<IEnumerable<Module>> ListAsyncPaged(int pageSize, int pageNumber);
    }
}
