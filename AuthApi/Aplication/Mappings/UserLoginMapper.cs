using AutoMapper;
using AuthApi.API.DTOs;
using DE = AuthApi.Domain.Entities;

namespace AuthApi.Application.Mappings
{
    public class UserLoginMapper : Profile
    {
        public UserLoginMapper()
        {
            CreateMap<DE.User, UserLogin>()
                .ReverseMap()
                ;
        }
    }
}