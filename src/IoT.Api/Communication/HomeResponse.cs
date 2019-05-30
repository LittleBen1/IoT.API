using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace IoT.Api.Communication
{
    public class HomeResponse : BaseResponse
    {
        public Home Home { get; private set; }

        private HomeResponse(bool success, string message, Home home) : base(success, message)
        {
            Home = home;
        }
        public HomeResponse(Home home) : this(true, string.Empty, home)
        { }
        public HomeResponse(string message) : this(false, message, null)
        { }
    }
}
