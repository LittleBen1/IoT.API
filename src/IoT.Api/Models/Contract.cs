using System;
using WebAPI.Models.EnumTypes;

namespace WebAPI.Models
{
    public class Contract
    {
        public int Id { get; set; }

        public int HomeId { get; set; }
        public Home Home { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ModuleId { get; set; }
        public Module Module { get; set; }

        public EContractType Type { get; set; }

        public Contract()
        {
        }
    }
}
