using AutoMapper;
using OblakotekaDTO;
using OblakotekaServer.Domain.Exceptions;
using OblakotekaServer.Domain.Models;

namespace OblakotekaServer.Domain
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly CancellationToken _token;
        private readonly IMapper _mapper;
        public ProductService(IMapper mapper, IProductRepository productRepository, CancellationToken token)
        {
            _productRepository = productRepository;
            _token = token;
            _mapper = mapper;
        }

        public async Task<ProductDTO[]> GetProductList(string? search)
        {
            var result = await _productRepository.FilterByName(search, _token);
            return result.Select(x => _mapper.Map<ProductDTO>(x)).ToArray();
        }

        public async Task<ProductDTO> Create(ProductCreateDTO dto)
        {
            var @params = _mapper.Map<ProductCreateParams>(dto);
            var result = await _productRepository.Create(@params);
            return _mapper.Map<ProductDTO>(result);
        }

        public async Task<ProductDTO> DeleteById(Guid id)
        {
            var result = await _productRepository.DeleteById(id) ?? throw new ProductNotFoundException(id);
            return _mapper.Map<ProductDTO>(result);
        }

        public async Task<ProductDTO> Edit(Guid id, ProductEditDto dto)
        {
            var @params = _mapper.Map<ProductEditParams>(dto);
            var result = await _productRepository.Edit(id, @params) ?? throw new ProductNotFoundException(id);
            return _mapper.Map<ProductDTO>(result);
        }
    }
}