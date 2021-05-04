using AutoMapper;
using COA.Data.Models;

namespace COA.Api.Resources
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserResource, Usuario>().ReverseMap();
        }
    }
}
