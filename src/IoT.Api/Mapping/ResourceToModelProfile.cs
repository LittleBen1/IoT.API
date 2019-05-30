using AutoMapper;
using IoT.Api.Models;
using IoT.Api.Resources;
using WebAPI.Models;

namespace IoT.Api.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveUserResource, User>();
            CreateMap<SaveHomeResource, Home>();
            CreateMap<SaveContractResource, Contract>();
            CreateMap<SaveModuleResource, Module>();
            CreateMap<UserCredentialsResource, User>();
        }        
    }
}
