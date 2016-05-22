using EHome.Core;
using EHome.Storage;

namespace EHome.Plugins.Esp
{
    public class EspPlugin : IPlugin
    {
        private readonly IEventBus _eventBus;
        private readonly IEHomeService _eHomeService;

        public EspPlugin(IEventBus eventBus, IEHomeService eHomeService)
        {
            _eventBus = eventBus;
            _eHomeService = eHomeService;
        }

        public void Init()
        {
            _eventBus.Subscribe(12, Execute);
        }

        private void Execute(HomeControlEventArgs obj)
        {
            _eHomeService.GetDevices();
            // publish event to Esp12
            _eventBus.Publish("esp" + obj.ModuleId, new byte[1] { 0 });
            // todo: store state to db.
        }
    }
}
