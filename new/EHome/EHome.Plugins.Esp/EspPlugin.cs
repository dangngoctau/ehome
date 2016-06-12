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
            var msg = new byte[eventArgs.Data.Length + 2];
            msg[0] = eventArgs.DeviceId;
            msg[1] = eventArgs.PropertyType;
            eventArgs.Data.CopyTo(msg, 2);
            _eventBus.Publish(PluginPrefix + eventArgs.ModuleId, msg);

            await _eHomeService.UpdateDeviceStateAsync(eventArgs.DeviceId, System.Text.Encoding.ASCII.GetString(eventArgs.Data));
        }
    }
}
