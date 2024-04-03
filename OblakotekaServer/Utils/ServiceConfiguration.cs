namespace OblakotekaServer.Utils
{
    public class ServiceConfiguration
    {
        [ConfigurationKeyName("DB_CONNECTION_STRING")]
        public string DbConnectionString {get;set;} = null!;
    }
}