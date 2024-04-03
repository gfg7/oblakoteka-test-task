namespace OblakotekaServer.Domain.Models
{
    public record ProductCreateParams(
        string Name,
        string? Description
    );
}