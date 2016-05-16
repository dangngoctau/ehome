using EHome.Services;
using Nancy;

namespace EHome.Modules
{
    public class HomeModule : NancyModule
    {
        private readonly IDeviceService _deviceService;

        public HomeModule(IDeviceService deviceService)
        {
            _deviceService = deviceService;

            Get["/"] = _ =>
            {
                return "The time is " + _deviceService.Test(new Services.Models.Device { Name = "1234" });
            };
        }
    }
}
