using IoT.Api.Repositories;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAPI.Models;

namespace IoT.Api.Persistence
{
    public class ContractRepository : BaseRepository, IContractRepository
    {
        public ContractRepository(MyDbContext context) : base(context) { }

        public async Task AddAsync(Contract contract)
        {
            await _context.Contracts.AddAsync(contract);
        }

        public async Task<Contract> FindByIdAsync(int id)
        {
            return await _context.Contracts.FindAsync(id);
        }

        public DbSet<Contract> GetContracts()
        {
            return _context.Contracts;
        }


        public async Task<IEnumerable<Contract>> ListAsync()
        {
            return await _context.Contracts.ToListAsync();
        }

        public void Remove(Contract contract)
        {
            _context.Contracts.Remove(contract);
        }

        public void Update(Contract contract)
        {
            _context.Contracts.Update(contract);
        }
    }
}
