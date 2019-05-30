using System;
using System.Collections.Generic;
using IoT.WebApp.Models;
using WebAPI.Models;

namespace IoT.WebApp.Models
{
    public class Home : IEntity
    {
        public int Id { get; set; }
        public EHomeType Type { get; set; }
        public string Address { get; set; }
        public string IpAddress { get; set; }

        public virtual ICollection<Module> Modules { get; set; } 
        public virtual ICollection<Contract> Contracts { get; set; }

        public Home()
        {
            Modules = new HashSet<Module>();
            Contracts = new HashSet<Contract>();
        }
    }
}