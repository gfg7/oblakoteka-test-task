using OblakotekaDTO;

namespace OblakotekaServer.Domain.Models
{
    public record ProductDomain(
        Guid Id,
        string Name,
        string? Description
    );

    public static class ProductDomainExtension
    {
        public static ProductDTO ToDTO(this ProductDomain model)
        {
            return new(model.Id, model.Name, model.Description);
        }
    }
}