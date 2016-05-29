using System.Threading.Tasks;
using EHome.Storage;
using Nancy;

namespace EHome.Core.Api
{
    public class HomeModule : NancyModule
    {
        private readonly IEHomeService _ehomeService;

        public HomeModule(IEHomeService ehomeService)
        {
            _ehomeService = ehomeService;

            Get["/api/devices", true] = async (ctx, ct) =>
            {
                var result = await _ehomeService.GetGroupDevicesAsync();
                return result;
            };
        }
    }
}
