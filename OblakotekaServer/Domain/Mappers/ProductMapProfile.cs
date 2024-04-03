using AutoMapper;
using OblakotekaDTO;
using OblakotekaServer.Domain.Models;

namespace OblakotekaServer.Domain.Mappers
{
    public class ProductMapProfile : Profile
    {
        public ProductMapProfile()
        {
            CreateMap<ProductDomain, ProductDTO>();
        }
    }
}