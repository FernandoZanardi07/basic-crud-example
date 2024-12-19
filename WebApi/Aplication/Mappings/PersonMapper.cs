using AutoMapper;
using ExampleCrud.WebApi.API.DTOs;
using DE = ExampleCrud.WebApi.Domain.Entities;

public class PeronMapper : Profile
{
    public PeronMapper()
    {
        CreateMap<Person, DE.Person>()
            .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => src.Contacts))
            ;

        CreateMap<DE.Person, Person>()
            .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => src.Contacts))
            ;
    }
}