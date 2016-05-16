using EHome.Common;
using System;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;

namespace EHome.Drivers.Implementations
{
    public class MqttDriver : IDriver
    {
        private readonly IAppSettings _appSettings;
        private MqttClient _client;

        public MqttDriver(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        private void _client_ConnectionClosed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public DriverType Type
        {
            get
            {
                return DriverType.Mqtt;
            }
        }

        public DateTime Test()
        {
            _client.Publish("test", new byte[] { 1, 1 });
            return DateTime.Now;
        }

        public void Start()
        {
            _client = StartMqttClient();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        #region private methods

        private MqttClient StartMqttClient()
        {
            MqttClient client;
            try
            {
                client = new MqttClient(_appSettings.BrokerAddress);
                client.Connect("host.api");

                return client;
            }
            catch (Exception ex)
            {
                Thread.Sleep(TimeSpan.FromSeconds(5));
                return StartMqttClient();
            }
        }

        #endregion private methods
    }
}
