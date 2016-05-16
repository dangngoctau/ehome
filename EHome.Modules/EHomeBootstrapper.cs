using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.TinyIoc;
using EHome.Drivers;
using EHome.Drivers.Implementations;
using Nancy.Bootstrapper;
using EHome.Services;
using EHome.Common;

namespace EHome.Modules
{
    public class EHomeBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            // Create db.
            Database.Create();

            // Start all drivers.
            var driverFactory = container.Resolve<IDriverFactory>();
            driverFactory.StartDrivers();
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            // todo: move IDriver registration to child Container.
            container.Register<IDeviceService, DeviceService>();
            container.Register<IAppSettings, AppSettings>().AsSingleton();
            container.RegisterMultiple<IDriver>(new[] { typeof(MqttDriver), typeof(Af24Driver) });
            container.Register<IDriverFactory, DriverFactory>().AsSingleton();
        }
    }
}
