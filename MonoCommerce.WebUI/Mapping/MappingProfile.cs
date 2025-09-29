using AutoMapper;
using MonoCommerce.Entities;
using MonoCommerce.WebUI.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SiteViewModel, Site>().ReverseMap();
        CreateMap<OrderViewModel, Order>().ReverseMap();

        CreateMap<Product, ProductViewModel>().ReverseMap();
        CreateMap<CargoCompany, CargoCompanyViewModel>().ReverseMap();
        CreateMap<Order, OrderViewModel>();

        // CreateMap<OrderViewModel, Order>()
        //     // .ForMember(dest => dest.Product, opt => opt.Ignore()) // sadece ProductId yeterli
        //     .ForMember(dest => dest.Site, opt => opt.Ignore())    // SiteId yeterli
        //     .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.UnitPrice));
    }
}