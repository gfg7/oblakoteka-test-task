using AutoMapper;
using OblakotekaDTO;
using OblakotekaServer.Domain.Models;

namespace OblakotekaServer.Domain.Mappers
{
    public class ProductCreateProfile : Profile
    {
        public ProductCreateProfile()
        {
            CreateMap<ProductCreateDTO, ProductCreateParams>();
        }
    }
}