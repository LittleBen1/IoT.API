using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.Api.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserRole> UsersRole { get; set; }

        public Role()
        {
            UsersRole = new Collection<UserRole>();
        }
    }
}
