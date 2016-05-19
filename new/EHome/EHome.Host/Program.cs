using System;
using System.Configuration;

namespace EHome.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Starting http gateway...");
            var host = new Nancy.Hosting.Self.NancyHost(new Uri(ConfigurationManager.AppSettings["HostUri"]));
            host.Start();
            Console.WriteLine("started!");

            // todo: start mqtt gateway
            Console.WriteLine("press any key to exit");
            Console.Read();

            //todo: test sqlite mono, dapper, m2mqtt, nancy api
        }
    }
}
