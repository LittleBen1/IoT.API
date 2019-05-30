using System;
using System.Collections.Generic;
using WebAPI.Models.EnumTypes;

namespace WebAPI.Models
{
    public class Home
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public EHomeType Type { get; set; }
        public string Address { get; set; }
        public string IpAddress { get; set; }
        public ICollection<Module> Modules { get; set; } 
        public ICollection<Contract> Contracts { get; set; }

        public Home()
        {
            Modules = new HashSet<Module>();
            Contracts = new HashSet<Contract>();
        }
    }
}