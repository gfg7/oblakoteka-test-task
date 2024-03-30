namespace OblakotekaServer.Domain.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(Guid id) : base($"Продукт с идентификатором {id} не найден")
        {
            
        }
    }
}