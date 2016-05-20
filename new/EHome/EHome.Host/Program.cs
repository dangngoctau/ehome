using EHome.Common;
using EHome.Infrastructure;
using EHome.Plugins.Esp;
using Nancy.TinyIoc;
using System;
using System.Configuration;

namespace EHome.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = TinyIoCContainer.Current;
            container.Register<IAppSettings, AppSettings>().AsSingleton();
            container.Register<IGateway, MqttGateway.MqttGateway>().AsSingleton();
            container.RegisterMultiple<IPlugin>(new[] { typeof(EspPlugin) }).AsSingleton();
            Console.Write("Starting http gateway...");
            var host = new Nancy.Hosting.Self.NancyHost(new Uri(ConfigurationManager.AppSettings["HostUri"]));
            host.Start();
            Console.WriteLine("started!");


            // todo: start mqtt gateway
            var gateway = container.Resolve<IGateway>();
            gateway.Start();

            Console.WriteLine("press any key to exit");
            Console.Read();

            //todo: test sqlite mono, dapper, m2mqtt, nancy api
        }
    }
}
