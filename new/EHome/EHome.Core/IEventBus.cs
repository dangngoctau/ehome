using System;

namespace EHome.Core
{
    public interface IEventBus
    {
        void Publish(string pluginId, byte[] message);
        void Subscribe(int pluginId, Action<HomeControlEventArgs> action);
    }
}