using IoT.Api.Communication;
using IoT.Api.Helpers;
using IoT.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;

namespace IoT.Api.Services
{

    public class ContractService : IContractService
    {

        //private readonly AppSettings _appsettings;
        private readonly IContractRepository _contractRepository;
        private IUnitOfWork _unitOfWork;

        public ContractService(IContractRepository contractRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _contractRepository = contractRepository;
           // _appsettings = appSettings.Value;
        }


        public IContractRepository Contracts()
        {
            return _contractRepository;
        }

        //public Contract Authenticate(string email, string password)
        //{
        //    var contract = _contracts.SingleOrDefault(x => x.EmailAddress == email && x.Password == password);

        //    if (contract == null)
        //        return null;

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appsettings.Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, contract.Id.ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    //contract.Token = tokenHandler.WriteToken(token);

        //    //remove password before returning
        //    contract.Password = null;

        //    return contract;
        //}

        public async Task<IEnumerable<Contract>> ListAsync()
        {
            return await _contractRepository.ListAsync();
        }

        public async Task<IEnumerable<Contract>> ListAsyncPaged(int pageSize = -1, int pageNumber = -1)
        {
            // Check if the source is null
            if (pageSize == -1 || pageNumber == -1)
                return await ListAsync();
            IEnumerable<Contract> source = _contractRepository.GetContracts();
            int count = source.Count();
            int CurrentPage = pageNumber;
            int PageSize = pageSize;
            int TotalCount = count;
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            IEnumerable<Contract> items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            var previousPage = CurrentPage > 1 ? "Yes" : "No";
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            //HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            // Returing List of Customers Collections  
            return items;
        }


        public async Task<ContractResponse> SaveAsync(Contract contract)
        {
            try
            {
                await _contractRepository.AddAsync(contract);
                await _unitOfWork.CompleteAsync();

                return new ContractResponse(contract);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ContractResponse($"An error occurred when saving the contract: {ex.Message}");
            }
        }

        public async Task<ContractResponse> UpdateAsync(int id, Contract contract)
        {
            var existingContract = await _contractRepository.FindByIdAsync(id);

            if (existingContract == null)
                return new ContractResponse("Contract not found.");
            if (existingContract.Id != id)
                return new ContractResponse("Id provided do not match the id of the contract");

            existingContract.HomeId = contract.HomeId;
            existingContract.UserId = contract.UserId;
            existingContract.ModuleId = contract.ModuleId;
            
            try
            {
                _contractRepository.Update(existingContract);
                await _unitOfWork.CompleteAsync();

                return new ContractResponse(existingContract);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ContractResponse($"An error occurred when updating the contract: {ex.Message}");
            }
        }

        public async Task<ContractResponse> DeleteAsync(int id)
        {
            var existingContract = await _contractRepository.FindByIdAsync(id);

            if (existingContract == null)
                return new ContractResponse("Contract not found.");

            try
            {
                _contractRepository.Remove(existingContract);
                await _unitOfWork.CompleteAsync();

                return new ContractResponse(existingContract);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ContractResponse($"An error occurred when deleting the contract: {ex.Message}");
            }
        }
    }
}

