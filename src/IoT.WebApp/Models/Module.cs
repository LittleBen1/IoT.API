using System;
using WebAPI.Models;

namespace IoT.WebApp.Models
{
    public class Module : IEntity
    {
        public int Id { get; set; }
        public EModuleType Type { get; set; }
        public int Port { get; set; }

        public int HomeId { get; set; }
        public virtual Home Home { get; set; }

        public Module()
        {
        }
    }
}