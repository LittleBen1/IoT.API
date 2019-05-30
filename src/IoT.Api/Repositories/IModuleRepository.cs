using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace IoT.Api.Repositories
{
    public interface IModuleRepository
    {
        Task<IEnumerable<Module>> ListAsync();
        Task AddAsync(Module module);
        Task<Module> FindByIdAsync(int id);
        void Update(Module module);
        void Remove(Module module);
        DbSet<Module> GetModules();
    }
}
