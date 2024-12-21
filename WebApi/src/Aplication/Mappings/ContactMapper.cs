using AutoMapper;
using ExampleCrud.WebApi.API.DTOs;
using DE = ExampleCrud.WebApi.Domain.Entities;

public class ContactMapper : Profile
{
    public ContactMapper()
    {
        CreateMap<DE.Contact, Contact>()
        .ReverseMap()
        ;
    }
}