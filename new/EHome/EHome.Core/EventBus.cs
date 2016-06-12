using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EHome.Common;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace EHome.Core
{
    public class EventBus : IEventBus
    {
        private readonly IAppSettings _appSettings;
        private readonly MqttClient _client;
        private readonly ConcurrentDictionary<int, HomeControlAction> _eventHandlers;

        public EventBus(IAppSettings appSettings)
        {
            _appSettings = appSettings;
            _eventHandlers = new ConcurrentDictionary<int, HomeControlAction>();
            _client = new MqttClient(_appSettings.BrokerAddress);
            _client.MqttMsgPublishReceived += _client_MqttMsgPublishReceived;
            _client.ConnectionClosed += _client_ConnectionClosed;
            StartClient();
        }

        private void _client_ConnectionClosed(object sender, EventArgs e)
        {
            StartClient();
        }

        private void StartClient()
        {
            try
            {
                _client.Connect("eventbus");
                _client.Subscribe(new[] { "eventbus" }, new[] { (byte)1 });
            }
            catch (MqttConnectionException ex)
            {
                StartClient();
            }
        }

        private async void _client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            switch (e.Topic)
            {
                case "eventbus":
                    var channel = e.Message[0];
                    var moduleId = e.Message[1];
                    var deviceId = e.Message[2];
                    var propertyType = e.Message[3];
                    var data = e.Message.Skip(4).ToArray();

                    var eventArgs = new HomeControlEventArgs
                    {
                        ModuleId = moduleId,
                        DeviceId = deviceId,
                        PropertyType = propertyType,
                        Data = data
                    };
                    foreach (var handler in _eventHandlers)
                    {
                        if (handler.Key == channel)
                        {
                            await handler.Value(eventArgs);
                        }
                    }
                    break;
                case "update":

                    break;
            }
        }

        public void Subscribe(int pluginId, HomeControlAction action)
        {
            _eventHandlers.TryAdd(pluginId, action);
        }

        public void Publish(string pluginId, byte[] message)
        {
            _client.Publish(pluginId, message);
        }
    }

    public delegate Task HomeControlAction(HomeControlEventArgs eventArgs);
}
