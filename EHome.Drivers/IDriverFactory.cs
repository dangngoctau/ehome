namespace EHome.Drivers
{
    public interface IDriverFactory
    {
        IDriver GetDriver(DriverType driverType);
        void StartDrivers();
    }
}
