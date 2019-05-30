using System;
using WebAPI.Models.EnumTypes;

namespace WebAPI.Models
{
    public class Module
    {
        public int Id { get; set; }
        public Home Home { get; set; }
        public EModuleType Type { get; set; }
        public int Port { get; set; }

        public Module()
        {
        }
    }
}