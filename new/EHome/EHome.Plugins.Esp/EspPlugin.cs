using EHome.Infrastructure;

namespace EHome.Plugins.Esp
{
    public class EspPlugin : IPlugin
    {
        public string DriverType
        {
            get
            {
                return "Esp";
            }
        }

        public void Execute(IRequest request)
        {

        }
    }
}
