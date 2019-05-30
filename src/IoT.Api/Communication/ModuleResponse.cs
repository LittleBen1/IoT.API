using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace IoT.Api.Communication
{
    public class ModuleResponse : BaseResponse
    {
        public Module Module { get; private set; }

        private ModuleResponse(bool success, string message, Module module) : base(success, message)
        {
            Module = module;
        }
        public ModuleResponse(Module module) : this(true, string.Empty, module)
        { }
        public ModuleResponse(string message) : this(false, message, null)
        { }
    }
}
