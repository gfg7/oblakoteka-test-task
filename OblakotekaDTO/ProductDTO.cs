namespace OblakotekaDTO
{
    public record ProductDTO(
        Guid Id,
        string Name,
        string? Description
    ) : ProductCreateDTO(Name, Description);
}