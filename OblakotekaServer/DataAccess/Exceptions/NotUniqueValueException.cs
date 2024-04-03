namespace OblakotekaServer.DataAccess.Exceptions
{
    public class NotUniqueValueException :Exception
    {
        public NotUniqueValueException(string value) : base($"'{value}' нарушает уникальность значений")
        {
            
        }
    }
}