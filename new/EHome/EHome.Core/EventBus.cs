using System;
using System.Collections.Concurrent;
using System.Text;
using EHome.Common;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace EHome.Core
{
    public class EventBus : IEventBus
    {
        private readonly IAppSettings _appSettings;
        private readonly MqttClient _client;
        private readonly ConcurrentDictionary<int, Action<HomeControlEventArgs>> _eventHandlers;

        public EventBus(IAppSettings appSettings)
        {
            _appSettings = appSettings;
            _eventHandlers = new ConcurrentDictionary<int, Action<HomeControlEventArgs>>();
            _client = new MqttClient(_appSettings.BrokerAddress);
            _client.Connect("eventbus");
            _client.Subscribe(new[] { "eventbus" }, new[] { (byte)1 });
            _client.MqttMsgPublishReceived += _client_MqttMsgPublishReceived;
        }

        private void _client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            // todo: Better implementation.
            var msg = Encoding.ASCII.GetString(e.Message);
            var channel = short.Parse(msg.Substring(0, 2).ToString());
            var moduleId = short.Parse(msg.Substring(2, 2).ToString());
            var deviceId = short.Parse(msg.Substring(4, 2).ToString());
            var state = short.Parse(msg.Substring(6, 2).ToString());
            var eventArgs = new HomeControlEventArgs
            {
                ModuleId = moduleId,
                DeviceId = deviceId,
                State = state
            };
            foreach (var handler in _eventHandlers)
            {
                if(handler.Key == channel)
                {
                    handler.Value(eventArgs);
                }
            }
        }

        public void Subscribe(int pluginId, Action<HomeControlEventArgs> action)
        {
            _eventHandlers.TryAdd(pluginId, action);
        }

        public void Publish(string pluginId, byte[] message)
        {
            _client.Publish(pluginId, message);
        }
    }
}
