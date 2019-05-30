using IoT.Api.Repositories;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAPI.Models;
using System.Linq;
using IoT.Api.Models;
using IoT.Api.Models.ETypes;

namespace IoT.Api.Persistence
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(MyDbContext context) : base(context) { }

        public async Task AddAsync(User user, ERole[] userRoles)
        {
            var roles = await _context.Roles.Where(r => userRoles.Any(ur => ur.ToString() == r.Name))
                                             .ToListAsync();

            foreach (var role in roles)
            {
                user.UserRoles.Add(new UserRole { RoleId = role.Id });
            }

            _context.Users.Add(user);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.Include(u => u.UserRoles)
                                     .ThenInclude(ur => ur.Role)
                                     .SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public DbSet<User> GetUsers()
        {
            return _context.Users;
        }


        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
