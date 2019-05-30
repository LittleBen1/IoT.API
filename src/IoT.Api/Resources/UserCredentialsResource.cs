using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.Api.Resources
{
   public class UserCredentialsResource
        {
            [Required]
            [DataType(DataType.EmailAddress)]
            [StringLength(255)]
            public string Email { get; set; }

            [Required]
            [StringLength(32)]
            public string Password { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
           
            public int ContractId { get; set; }

    }
}
