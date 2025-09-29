using AutoMapper;
using MonoCommerce.Entities;
using MonoCommerce.WebUI.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SiteViewModel, Site>().ReverseMap();

        CreateMap<Product, ProductViewModel>().ReverseMap();
        CreateMap<CargoCompany, CargoCompanyViewModel>().ReverseMap();

    }
}