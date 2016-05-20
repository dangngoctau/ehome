using EHome.Common;
using EHome.Infrastructure;
using System.Collections.Generic;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace EHome.MqttGateway
{
    public class MqttGateway : IGateway
    {
        private readonly IAppSettings _appSettings;
        private readonly IEnumerable<IPlugin> _plugins;

        private MqttClient _client;

        public MqttGateway(IAppSettings appSettings, IEnumerable<IPlugin> plugins)
        {
            _appSettings = appSettings;
            _client = new MqttClient(_appSettings.BrokerAddress);
            _plugins = plugins;
        }

        public void Start()
        {
            // todo: move to config.
            _client.MqttMsgPublishReceived += ClientMqttMsgPublishReceived;
            _client.Connect("gateway");
            _client.Subscribe(new[] { "changestate" }, new[] { (byte)1 });
        }

        private void ClientMqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //    Request: { Channel | Driver}/{ module - code}/{ DeviceId}/{ State} | topic: "changestate", msg: [1byte][1byte][8bytes][1byte]
            //-- -
            //Gateway: Receive:
            //    -Get driver type of device => esp
            //   - EspPlugin puslishs msg: topic: "esp{module-code|int}", msg: [DeviceId][State]
            // Get registered action in list, invoke that action.

            if (e.Topic == "changestate")
            {
                // get channel, moule code.....
                // c
            }
            var request = new EHomeRequest { PluginId = 1, Topic = e.Topic, Message = e.Message };
            foreach (var plugin in _plugins)
            {
                if (plugin.Id == request.PluginId)
                {
                    plugin.Execute(request);
                }
            }
        }

        public void Stop()
        {
            _client.Disconnect();
        }
    }
}
