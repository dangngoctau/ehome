using Dapper;
using EHome.Drivers;
using EHome.Services.Models;
using System.Linq;

namespace EHome.Services
{
    public class DeviceService : ServiceBase, IDeviceService
    {
        private readonly IDriverFactory _driverFactory;

        public DeviceService(IDriverFactory driverFactory)
        {
            _driverFactory = driverFactory;
        }

        public long Test(Device device)
        {
            _driverFactory.GetDriver(DriverType.Mqtt).Test();
            using (var cnn = GetConnection())
            {
                cnn.Open();
                var id = cnn.Query<long>(@"INSERT INTO Devices (Name) VALUES (@Name); select last_insert_rowid()", device).First();

                return id;
            }
        }
    }
}
