using AutoMapper;
using MonoCommerce.Entities;
using MonoCommerce.WebUI.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductViewModel>().ReverseMap();
    }
}