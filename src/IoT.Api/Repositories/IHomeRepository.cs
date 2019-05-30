using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace IoT.Api.Repositories
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Home>> ListAsync();
        Task AddAsync(Home home);
        Task<Home> FindByIdAsync(int id);
        void Update(Home home);
        void Remove(Home home);
        DbSet<Home> GetHomes();
    }
}
