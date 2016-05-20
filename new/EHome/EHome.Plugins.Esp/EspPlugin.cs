using EHome.Infrastructure;

namespace EHome.Plugins.Esp
{
    public class EspPlugin : IPlugin
    {
        public int Id
        {
            get
            {
                return 1;
            }
        }

        public void Execute(IRequest request)
        {
            var i = request;
        }
    }
}
