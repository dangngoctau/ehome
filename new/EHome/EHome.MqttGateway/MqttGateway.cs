using EHome.Common;
using EHome.Infrastructure;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

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
            _client.MqttMsgPublishReceived += _client_MqttMsgPublishReceived;
            _client.Connect("gateway");
        }

        public void RegisterEventAction()
        {
            // todo: IRequest, IReponse, ex: if event Turn on device 1, module Relay, PluginRelay is invoked.
        }

        private void _client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            // Get registered action in list, invoke that action.
        }

        public void Stop()
        {
            _client.Disconnect();
        }
    }
}
