using IoT.Api.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace IoT.Api.Services
{
    public interface IHomeService
    {
        Task<IEnumerable<Home>> ListAsync();
        Task<HomeResponse> SaveAsync(Home home);
        Task<HomeResponse> UpdateAsync(int id, Home home);
        Task<HomeResponse> DeleteAsync(int id);
        Task<IEnumerable<Home>> ListAsyncPaged(int pageSize, int pageNumber);
    }
}
