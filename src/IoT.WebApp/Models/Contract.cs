using System;
using WebAPI.Models;

namespace IoT.WebApp.Models
{
    public class Contract : IEntity
    {
        public int Id { get; set; }
        
        public virtual int HomeId { get; set; }
        public virtual Home Home { get; set; }

        public virtual int UserId { get; set; }
        public virtual User User { get; set; }

        public EContractType Type { get; set; }

        public Contract()
        {
        }
    }
}
