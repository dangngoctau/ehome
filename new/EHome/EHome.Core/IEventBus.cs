namespace EHome.Core
{
    public interface IEventBus
    {
        void Publish(string pluginId, byte[] message);
        void Subscribe(int pluginId, HomeControlAction action);
    }
}