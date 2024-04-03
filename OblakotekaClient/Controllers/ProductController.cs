using Microsoft.AspNetCore.Mvc;
using OblakotekaClient.Services;
using OblakotekaDTO;

namespace OblakotekaClient.Controllers;

[Controller]
[Route("product")]
public class ProductController : Controller
{
    private readonly ProductServiceClient _productService;
    public ProductController(ProductServiceClient productService)
    {
        _productService = productService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("find")]
    public async Task<IActionResult> Find([FromQuery] string search)
    {
        var result = await _productService.GetProducts(search);
        return PartialView("ProductTablePartial", result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ProductCreateDTO dto)
    {
        await _productService.CreateProduct(dto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit([FromRoute] Guid id, [FromForm] ProductEditDto dto)
    {
        await _productService.UpdateProduct(id, dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _productService.DeleteProduct(id);
        return Ok();
    }
}
