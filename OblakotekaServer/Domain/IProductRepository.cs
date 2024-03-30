using OblakotekaServer.Domain.Models;

namespace OblakotekaServer.Domain
{
    public interface IProductRepository
    {
        Task<ProductDomain> Create(ProductCreateParams @params);
        Task<ProductDomain?> DeleteById(Guid id);
        Task<ProductDomain?> Edit(Guid id, ProductEditParams @params);
        Task<ProductDomain[]> FilterByName(string search);
    }
}