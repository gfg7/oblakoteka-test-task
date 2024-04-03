using OblakotekaDTO;

namespace OblakotekaServer.Domain.Models
{
    public record ProductDomain(
        Guid Id,
        string Name,
        string? Description
    );
}