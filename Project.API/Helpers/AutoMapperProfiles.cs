using AutoMapper;
using Project.API.DTOs;
using Project.API.Models;

namespace Project.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AccountForRegisterDto, Account>().ReverseMap();
            CreateMap<AccountToReturnDto, Account>().ReverseMap();
            CreateMap<AccountForUpdateDto, Account>().ReverseMap();
        }
    }
}