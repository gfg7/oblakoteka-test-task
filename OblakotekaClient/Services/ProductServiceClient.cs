using OblakotekaDTO;

namespace OblakotekaClient.Services
{
    public class ProductServiceClient
    {
        private readonly HttpClient _client;

        public ProductServiceClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<ProductDTO?> UpdateProduct(Guid id, ProductEditDto edittedProduct)
        {
            var response = await _client.PutAsJsonAsync($"/api/product/{id}", edittedProduct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ProductDTO>();
        }

         public async Task<ProductDTO[]> GetProducts(string? search=null)
        {
            var response = await _client.GetAsync($"/api/product?search={search}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ProductDTO[]>() ?? Array.Empty<ProductDTO>();
        }

        public async Task<ProductDTO?> CreateProduct(ProductCreateDTO newProduct)
        {
            var response = await _client.PostAsJsonAsync("/api/product", newProduct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ProductDTO>();
        }

        public async Task<ProductDTO?> DeleteProduct(Guid id)
        {
            var response = await _client.DeleteAsync($"/api/product/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ProductDTO>();
        }
    }
}