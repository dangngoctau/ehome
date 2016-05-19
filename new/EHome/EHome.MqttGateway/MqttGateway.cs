using EHome.Common;
using EHome.Infrastructure;
using uPLibrary.Networking.M2Mqtt;

namespace EHome.MqttGateway
{
    public class MqttGateway : IGateway
    {
        private readonly IAppSettings _appSettings;

        private MqttClient _client;

        public MqttGateway(IAppSettings appSettings)
        {
            _appSettings = appSettings;
            _client = new MqttClient(_appSettings.BrokerAddress);
        }

        public void Start()
        {
            // todo: move to config.
            _client.Connect("gateway");
        }

        public void Stop()
        {
            _client.Disconnect();
        }
    }
}
