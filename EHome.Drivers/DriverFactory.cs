using System;
using System.Collections.Generic;
using System.Linq;

namespace EHome.Drivers
{
    public class DriverFactory : IDriverFactory
    {
        private readonly IEnumerable<IDriver> _drivers;

        public DriverFactory(IEnumerable<IDriver> drivers)
        {
            _drivers = drivers;
        }

        public IDriver GetDriver(DriverType driverType)
        {
            switch (driverType)
            {
                case DriverType.Mqtt:
                    return _drivers.Single(c => c.Type == DriverType.Mqtt);
                case DriverType.Af24:
                    return _drivers.Single(c => c.Type == DriverType.Af24);
                default:
                    throw new NotSupportedException();
            }
        }

        public void StartDrivers()
        {
            foreach(var driver in _drivers)
            {
                driver.Start();
            }
        }
    }
}
