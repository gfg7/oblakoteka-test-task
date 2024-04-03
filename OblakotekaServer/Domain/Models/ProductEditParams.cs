namespace OblakotekaServer.Domain.Models
{
    public record ProductEditParams(
        string Name,
        string? Description
    );
}