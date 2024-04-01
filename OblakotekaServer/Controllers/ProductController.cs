using Microsoft.AspNetCore.Mvc;
using OblakotekaDTO;
using OblakotekaServer.Domain;
using OblakotekaServer.Domain.Models;

namespace OblakotekaServer.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController
    {
        private readonly ProductService _service;
        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ProductDTO[]> GetProductList([FromQuery] string? search=null)
        {
            var result = await _service.GetProductList(search);
            return result.Select(x => x.ToDTO()).ToArray();
        }

        [HttpPost]
        public async Task<ProductDTO> CreateProduct([FromBody] ProductCreateDTO dto)
        {
            var result = await _service.Create(dto.ToDomain());
            return result.ToDTO();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ProductDTO> EditProduct([FromRoute] Guid id, [FromBody] ProductEditDto dto)
        {
            var result = await _service.Edit(id, dto.ToDomain());
            return result.ToDTO();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ProductDTO> DeleteProduct([FromRoute] Guid id)
        {
            var result = await _service.DeleteById(id);
            return result.ToDTO();
        }
    }
}