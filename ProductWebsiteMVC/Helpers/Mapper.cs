using AutoMapper;
using ProductData.Models;
using ProductWebsiteMVC.ViewModels;

namespace ProductWebsiteMVC.Helpers
{
    public class Mapper : Profile
    {
        
        public Mapper()
        {
            CreateMap<ManufacturerViewModel, Manufacturer>().ReverseMap();
            CreateMap<ProductViewModel, Product>().ReverseMap();
        }
    }
}
