using System;
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

        public int BrokerPort
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["BrokerPort"]);
            }
        }
    }
}
