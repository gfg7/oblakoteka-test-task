using Microsoft.AspNetCore.Mvc;
using OblakotekaDTO;

namespace OblakotekaServer.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductVersionController
    {
        public ProductVersionController()
        {

        }

        [HttpGet]
        public Task<ProductDTO[]> GetProductList([FromQuery] string search)
        {
            return null;
        }

        [HttpPost]
        public Task<ProductDTO> CreateProduct([FromBody] ProductCreateDTO dto)
        {
            return null;
        }

        [HttpPut]
        [Route("{id}")]
        public Task<ProductDTO> EditProduct([FromRoute] Guid id, [FromBody] ProductEditDto dto)
        {
            return null;
        }

        [HttpDelete]
        [Route("{id}")]
        public Task<ProductDTO> DeleteProduct([FromRoute] Guid id)
        {
            return null;
        }
    }
}