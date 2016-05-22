using EHome.Common;
using EHome.Plugins.Esp;
using Nancy.TinyIoc;
using System;
using System.Configuration;
using EHome.Storage;
using EHome.Core;
using System.Collections.Generic;

namespace EHome.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = TinyIoCContainer.Current;
            if(Type.GetType("Mono.Runtime") == null)
            {
                container.Register<IDbConnectionSelection, WindowsDbConnectionSelection>().AsSingleton();
            }
            else
            {
                container.Register<IDbConnectionSelection, MonoDbConnectionSelection>().AsSingleton();
            }
            container.Register<IAppSettings, AppSettings>().AsSingleton();
            container.Register<IPluginHandler, PluginHandler>().AsSingleton();
            container.Register<IEventBus, EventBus>().AsSingleton();
            container.RegisterMultiple<IPlugin>(new[] { typeof(EspPlugin) }).AsSingleton();
            container.Register<IEHomeService, EHomeService>();
            Console.Write("Starting http gateway...");
            var host = new Nancy.Hosting.Self.NancyHost(new Uri(ConfigurationManager.AppSettings["HostUri"]));
            host.Start();
            Console.WriteLine("started!");

            var gateway = container.Resolve<IPluginHandler>();
            gateway.Init();

            Console.WriteLine("press any key to exit");
            Console.Read();

            //todo: test sqlite mono, dapper, m2mqtt, nancy api
        }
    }
}
