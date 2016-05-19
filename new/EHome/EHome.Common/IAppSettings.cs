namespace EHome.Common
{
    public interface IAppSettings
    {
        string BrokerAddress { get; }
        int BrokerPort { get; }
    }
}
