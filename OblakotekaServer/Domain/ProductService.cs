using OblakotekaServer.Domain.Exceptions;
using OblakotekaServer.Domain.Models;

namespace OblakotekaServer.Domain
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDomain[]> FilterByName(string search)
        {
            return await _productRepository.FilterByName(search);
        }

        public async Task<ProductDomain> Create(ProductCreateParams @params)
        {
            return await _productRepository.Create(@params);
        }

        public async Task<ProductDomain> DeleteById(Guid id)
        {
            var result = await _productRepository.DeleteById(id) ?? throw new ProductNotFoundException(id);
            return result;
        }

        public async Task<ProductDomain> Edit(Guid id, ProductEditParams @params)
        {
            var result = await _productRepository.Edit(id, @params) ?? throw new ProductNotFoundException(id);
            return result;
        }
    }
}