using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace IoT.Api.Communication
{
    public class ContractResponse : BaseResponse
    {
        public Contract Contract { get; private set; }

        private ContractResponse(bool success, string message, Contract contract) : base(success, message)
        {
            Contract = contract;
        }
        public ContractResponse(Contract contract) : this(true, string.Empty, contract)
        { }
        public ContractResponse(string message) : this(false, message, null)
        { }
    }
}
