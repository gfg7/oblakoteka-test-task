using OblakotekaDTO;

namespace OblakotekaServer.Domain.Models
{
    public record ProductCreateParams(
        string Name,
        string? Description
    );

    public static class ProductCreateParamsExtension
    {
        public static ProductCreateParams ToDomain(this ProductCreateDTO dto)
        {
            return new(dto.Name, dto.Description);
        }
    }
}