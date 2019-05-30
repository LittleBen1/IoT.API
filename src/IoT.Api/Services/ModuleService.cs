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

    public class ModuleService : IModuleService
    {

        //private readonly AppSettings _appsettings;
        private readonly IModuleRepository _ModuleRepository;
        private IUnitOfWork _unitOfWork;

        public ModuleService(IModuleRepository ModuleRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _ModuleRepository = ModuleRepository;
           // _appsettings = appSettings.Value;
        }

        public IModuleRepository Modules()
        {
            return _ModuleRepository;
        }

        public async Task<IEnumerable<Module>> ListAsync()
        {
            return await _ModuleRepository.ListAsync();
        }

        public async Task<IEnumerable<Module>> ListAsyncPaged(int pageSize = -1, int pageNumber = -1)
        {
            // Check if the source is null
            if (pageSize == -1 || pageNumber == -1)
                return await ListAsync();
            IEnumerable<Module> source = _ModuleRepository.GetModules();
            int count = source.Count();
            int CurrentPage = pageNumber;
            int PageSize = pageSize;
            int TotalCount = count;
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            IEnumerable<Module> items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            var previousPage = CurrentPage > 1 ? "Yes" : "No";
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            //HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            // Returing List of Customers Collections  
            return items;
        }


        public async Task<ModuleResponse> SaveAsync(Module Module)
        {
            try
            {
                await _ModuleRepository.AddAsync(Module);
                await _unitOfWork.CompleteAsync();

                return new ModuleResponse(Module);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ModuleResponse($"An error occurred when saving the Module: {ex.Message}");
            }
        }

        public async Task<ModuleResponse> UpdateAsync(int id, Module Module)
        {
            var existingModule = await _ModuleRepository.FindByIdAsync(id);

            if (existingModule == null)
                return new ModuleResponse("Module not found.");
            if (existingModule.Id != id)
                return new ModuleResponse("Id provided do not match the id of the Module");

            existingModule.Home = Module.Home;
            existingModule.Port = Module.Port;
            existingModule.Type = Module.Type;

            try
            {
                _ModuleRepository.Update(existingModule);
                await _unitOfWork.CompleteAsync();

                return new ModuleResponse(existingModule);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ModuleResponse($"An error occurred when updating the Module: {ex.Message}");
            }
        }

        public async Task<ModuleResponse> DeleteAsync(int id)
        {
            var existingModule = await _ModuleRepository.FindByIdAsync(id);

            if (existingModule == null)
                return new ModuleResponse("Module not found.");

            try
            {
                _ModuleRepository.Remove(existingModule);
                await _unitOfWork.CompleteAsync();

                return new ModuleResponse(existingModule);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ModuleResponse($"An error occurred when deleting the Module: {ex.Message}");
            }
        }
    }
}

