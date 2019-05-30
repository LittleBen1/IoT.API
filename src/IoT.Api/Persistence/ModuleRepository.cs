using IoT.Api.Repositories;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAPI.Models;

namespace IoT.Api.Persistence
{
    public class ModuleRepository : BaseRepository, IModuleRepository
    {
        public ModuleRepository(MyDbContext context) : base(context) { }

        public async Task AddAsync(Module module)
        {
            await _context.Modules.AddAsync(module);
        }

        public async Task<Module> FindByIdAsync(int id)
        {
            return await _context.Modules.FindAsync(id);
        }

        public DbSet<Module> GetModules()
        {
            return _context.Modules;
        }


        public async Task<IEnumerable<Module>> ListAsync()
        {
            return await _context.Modules.ToListAsync();
        }

        public void Remove(Module module)
        {
            _context.Modules.Remove(module);
        }

        public void Update(Module module)
        {
            _context.Modules.Update(module);
        }
    }
}
