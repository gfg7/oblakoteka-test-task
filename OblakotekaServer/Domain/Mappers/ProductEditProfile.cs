using AutoMapper;
using OblakotekaDTO;
using OblakotekaServer.Domain.Models;

namespace OblakotekaServer.Domain.Mappers
{
    public class ProductEditProfile : Profile
    {
        public ProductEditProfile()
        {
            CreateMap<ProductEditDto, ProductEditParams>();
        }
    }
}