using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoT.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Home> Homes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Module> Modules { get; set; }

    }
}
