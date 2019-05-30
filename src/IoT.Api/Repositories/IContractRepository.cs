using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace IoT.Api.Repositories
{
    public interface IContractRepository
    {
        Task<IEnumerable<Contract>> ListAsync();
        Task AddAsync(Contract contract);
        Task<Contract> FindByIdAsync(int id);
        void Update(Contract contract);
        void Remove(Contract contract);
        DbSet<Contract> GetContracts();
    }
}
