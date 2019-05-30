using IoT.Api.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WebAPI.Models
{
    public class User 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
       

        public virtual ICollection<Contract> Contracts { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

        public User()
        {
            UserRoles = new Collection<UserRole>();
            Contracts = new HashSet<Contract>();
        }
    }
}
