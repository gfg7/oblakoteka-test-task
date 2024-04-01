using OblakotekaServer.Domain.Exceptions;
using OblakotekaServer.Domain.Models;

namespace OblakotekaServer.Domain
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly CancellationToken _token;
        public ProductService(IProductRepository productRepository, CancellationToken token)
        {
            _productRepository = productRepository;
            _token = token;
        }

        public async Task<ProductDomain[]> GetProductList(string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return await _productRepository.GetProducts(_token);
            }

            return await _productRepository.FilterByName(search, _token);
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