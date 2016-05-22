using System.Collections.Generic;

namespace EHome.Core
{
    public class PluginHandler : IPluginHandler
    {
        private readonly IEnumerable<IPlugin> _plugins;

        public PluginHandler(IEnumerable<IPlugin> plugins)
        {
            _plugins = plugins;
        }

        public void Init()
        {
            foreach(var plugin in _plugins)
            {
                plugin.Init();
            }
        }
    }
}
