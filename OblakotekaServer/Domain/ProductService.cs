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
            return await _productRepository.DeleteById(id);
        }

        public async Task<ProductDomain> Edit(Guid id, ProductEditParams @params)
        {
            return await _productRepository.Edit(id, @params);
        }
    }
}