using OblakotekaDTO;

namespace OblakotekaServer.Domain.Models
{
     public record ProductEditParams(
        string Name,
        string? Description
    );

    public static class ProductEditParamsExtension
    {
        public static ProductEditParams ToDomain(this ProductEditDto dto)
        {
            return new(dto.Name, dto.Description);
        }
    }
}