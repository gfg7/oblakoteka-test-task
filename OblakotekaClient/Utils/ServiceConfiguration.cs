namespace OblakotekaClient.Utils
{
     public class ServiceConfiguration
    {
        [ConfigurationKeyName("SERVER_URL")]
        public string ServerBaseUrl {get;set;} = null!;
    }
}