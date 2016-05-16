using System.Configuration;

namespace EHome.Common
{
    public class AppSettings : IAppSettings
    {
        public string BrokerAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["BrokerAddress"];
            }
        }
    }
}
