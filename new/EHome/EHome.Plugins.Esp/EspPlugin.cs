using System.Threading.Tasks;
using EHome.Core;
using EHome.Storage;

namespace EHome.Plugins.Esp
{
    public class EspPlugin : IPlugin
    {
        private const string PluginPrefix = "esp";
        private const int PluginId = 12;

        private readonly IEventBus _eventBus;
        private readonly IEHomeService _eHomeService;

        public EspPlugin(IEventBus eventBus, IEHomeService eHomeService)
        {
            _eventBus = eventBus;
            _eHomeService = eHomeService;
        }

        public void Init()
        {
            _eventBus.Subscribe(PluginId, Execute);
        }

        private async Task Execute(HomeControlEventArgs eventArgs)
        {
            switch (eventArgs.DeviceType)
            {
                case DeviceType.Relay:
                    // Publish event to module esp<number-one-byte>
                    var msg = new byte[eventArgs.Data.Length + 1];
                    msg[0] = eventArgs.DeviceId;
                    eventArgs.Data.CopyTo(msg, 1);
                    _eventBus.Publish(PluginPrefix + eventArgs.ModuleId, msg);
                    break;
            }

            await _eHomeService.UpdateDeviceStateAsync(eventArgs.DeviceId, System.Text.Encoding.ASCII.GetString(eventArgs.Data));
        }
    }
}
