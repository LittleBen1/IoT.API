using IoT.Api.Repositories;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAPI.Models;

namespace IoT.Api.Persistence
{
    public class HomeRepository : BaseRepository, IHomeRepository
    {
        public HomeRepository(MyDbContext context) : base(context) { }

        public async Task AddAsync(Home home)
        {
            await _context.Homes.AddAsync(home);
        }

        public async Task<Home> FindByIdAsync(int id)
        {
            return await _context.Homes.FindAsync(id);
        }

        public DbSet<Home> GetHomes()
        {
            return _context.Homes;
        }


        public async Task<IEnumerable<Home>> ListAsync()
        {
            return await _context.Homes.ToListAsync();
        }

        public void Remove(Home home)
        {
            _context.Homes.Remove(home);
        }

        public void Update(Home home)
        {
            _context.Homes.Update(home);
        }
    }
}
