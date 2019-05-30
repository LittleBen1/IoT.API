using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace IoT.Api.Persistence
{
    public class BaseRepository
    {
        protected readonly MyDbContext _context;

        public BaseRepository(MyDbContext context)
        {
            _context = context;
        }
    }
}
