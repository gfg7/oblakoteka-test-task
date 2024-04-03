namespace OblakotekaServer.Domain.Exceptions
{
    public class NotUniqueValueException :Exception
    {
        public NotUniqueValueException(string value) : base($"'{value}' нарушает уникальность значений")
        {
            
        }
    }
}