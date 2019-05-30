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

    public class HomeService : IHomeService
    {

        //private readonly AppSettings _appsettings;
        private readonly IHomeRepository _homeRepository;
        private IUnitOfWork _unitOfWork;

        public HomeService(IHomeRepository homeRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _homeRepository = homeRepository;
           // _appsettings = appSettings.Value;
        }

        public IHomeRepository Homes()
        {
            return _homeRepository;
        }

        //public Home Authenticate(string email, string password)
        //{
        //    var home = _homes.SingleOrDefault(x => x.EmailAddress == email && x.Password == password);

        //    if (home == null)
        //        return null;

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appsettings.Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, home.Id.ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    //home.Token = tokenHandler.WriteToken(token);

        //    //remove password before returning
        //    home.Password = null;

        //    return home;
        //}

        public async Task<IEnumerable<Home>> ListAsync()
        {
            return await _homeRepository.ListAsync();
        }

        public async Task<IEnumerable<Home>> ListAsyncPaged(int pageSize = -1, int pageNumber = -1)
        {
            // Check if the source is null
            if (pageSize == -1 || pageNumber == -1)
                return await ListAsync();
            IEnumerable<Home> source = _homeRepository.GetHomes();
            int count = source.Count();
            int CurrentPage = pageNumber;
            int PageSize = pageSize;
            int TotalCount = count;
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            IEnumerable<Home> items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            var previousPage = CurrentPage > 1 ? "Yes" : "No";
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            //HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            // Returing List of Customers Collections  
            return items;
        }


        public async Task<HomeResponse> SaveAsync(Home home)
        {
            try
            {
                await _homeRepository.AddAsync(home);
                await _unitOfWork.CompleteAsync();

                return new HomeResponse(home);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new HomeResponse($"An error occurred when saving the home: {ex.Message}");
            }
        }

        public async Task<HomeResponse> UpdateAsync(int id, Home home)
        {
            var existingHome = await _homeRepository.FindByIdAsync(id);

            if (existingHome == null)
                return new HomeResponse("Home not found.");
            if (existingHome.Id != id)
                return new HomeResponse("Id provided do not match the id of the home");

            existingHome.Address = home.Address;
            existingHome.Contracts = home.Contracts;
            existingHome.IpAddress = home.IpAddress;
            existingHome.Name = home.Name;
            existingHome.Type = home.Type;
            existingHome.Modules = home.Modules;

            try
            {
                _homeRepository.Update(existingHome);
                await _unitOfWork.CompleteAsync();

                return new HomeResponse(existingHome);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new HomeResponse($"An error occurred when updating the home: {ex.Message}");
            }
        }

        public async Task<HomeResponse> DeleteAsync(int id)
        {
            var existingHome = await _homeRepository.FindByIdAsync(id);

            if (existingHome == null)
                return new HomeResponse("Home not found.");

            try
            {
                _homeRepository.Remove(existingHome);
                await _unitOfWork.CompleteAsync();

                return new HomeResponse(existingHome);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new HomeResponse($"An error occurred when deleting the home: {ex.Message}");
            }
        }
    }
}

